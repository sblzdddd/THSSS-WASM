param(
  [Parameter(Mandatory = $true)][string]$Repo,
  [Parameter(Mandatory = $true)][string]$Dest,
  [switch]$SkipCopy
)
$ErrorActionPreference = "Stop"
$Repo = (Resolve-Path -LiteralPath $Repo).Path.TrimEnd('\', '/')
$required = @("Image", "CS", "FX", "Model", "Story", "Sound", "BGM")
$optional = @("Replay")
$packRoots = @("Image", "CS", "FX", "Model", "Story", "Replay")
$audioRoots = @("BGM", "Sound")
$audioExt = @(".wav", ".mp3", ".ogg", ".xwma", ".flac", ".wma", ".mid", ".midi", ".m4a", ".aac")

$problems = New-Object System.Collections.Generic.List[string]
foreach ($root in $required) {
  $from = Join-Path $Repo $root
  if (-not (Test-Path -LiteralPath $from -PathType Container)) {
    $problems.Add("missing folder $root")
    continue
  }
  $any = Get-ChildItem -LiteralPath $from -Recurse -File -ErrorAction SilentlyContinue | Select-Object -First 1
  if (-not $any) {
    $problems.Add("empty folder $root")
  }
}
if ($problems.Count -gt 0) {
  throw ("Game content check failed: " + ($problems -join "; ") + ". Restore the gitignored asset folders at the repo root, or build with -p:StageGameAssets=false.")
}

$dat = Join-Path $Dest "thsss.dat"
if ($SkipCopy -and (Test-Path -LiteralPath $dat -PathType Leaf)) {
  Write-Host "Game content validated; staged archive already present."
  return
}

function Write-U16([System.IO.Stream]$stream, [int]$value) {
  $stream.WriteByte([byte]($value -band 255))
  $stream.WriteByte([byte](($value -shr 8) -band 255))
}

function Write-U32([System.IO.Stream]$stream, [long]$value) {
  $stream.WriteByte([byte]($value -band 255))
  $stream.WriteByte([byte](($value -shr 8) -band 255))
  $stream.WriteByte([byte](($value -shr 16) -band 255))
  $stream.WriteByte([byte](($value -shr 24) -band 255))
}

function Test-AudioPath([string]$rel) {
  foreach ($root in $audioRoots) {
    if ($rel.StartsWith($root + "/", [System.StringComparison]::OrdinalIgnoreCase)) { return $true }
  }
  $ext = [System.IO.Path]::GetExtension($rel)
  return $audioExt -contains $ext.ToLowerInvariant()
}

$utf8 = [System.Text.UTF8Encoding]::new($false)
$entries = New-Object System.Collections.Generic.List[object]
foreach ($root in $packRoots) {
  $from = Join-Path $Repo $root
  if (-not (Test-Path -LiteralPath $from -PathType Container)) { continue }
  Get-ChildItem -LiteralPath $from -Recurse -File | ForEach-Object {
    $rel = $_.FullName.Substring($Repo.Length).TrimStart('\', '/').Replace('\', '/')
    if (Test-AudioPath $rel) { return }
    $entries.Add([pscustomobject]@{ Rel = $rel; Full = $_.FullName; Bytes = $null; Size = [long]$_.Length })
  }
}
$score = Join-Path $Repo "Score.dat"
if (Test-Path -LiteralPath $score -PathType Leaf) {
  $info = Get-Item -LiteralPath $score
  $entries.Add([pscustomobject]@{ Rel = "Score.dat"; Full = $info.FullName; Bytes = $null; Size = [long]$info.Length })
}

$soundNames = New-Object System.Collections.Generic.List[string]
$soundDir = Join-Path $Repo "Sound"
Get-ChildItem -LiteralPath $soundDir -File -Filter "*.wav" | ForEach-Object { $soundNames.Add("Sound/" + $_.Name) }
$soundNames.Sort([System.StringComparer]::Ordinal)
$soundBytes = $utf8.GetBytes(($soundNames -join "`n") + "`n")
$entries.Add([pscustomobject]@{ Rel = "_thsss/sound.txt"; Full = $null; Bytes = $soundBytes; Size = [long]$soundBytes.Length })

if ($entries.Count -eq 0) {
  throw "Game content check failed: no files were packed. Restore the gitignored asset folders at the repo root, or build with -p:StageGameAssets=false."
}

$sorted = $entries | Sort-Object Rel
$indexSize = 0
foreach ($entry in $sorted) {
  $nameLen = $utf8.GetByteCount([string]$entry.Rel)
  if ($nameLen -gt 65535) { throw "Archive path is too long: $($entry.Rel)" }
  if ($entry.Size -gt 4294967295) { throw "Archive file is too large: $($entry.Rel)" }
  $indexSize += 10 + $nameLen
}
$dataStart = 12 + $indexSize
$cursor = [long]$dataStart
foreach ($entry in $sorted) {
  $entry | Add-Member -NotePropertyName Offset -NotePropertyValue $cursor -Force
  $cursor += $entry.Size
}

if (Test-Path -LiteralPath $Dest) { Remove-Item -LiteralPath $Dest -Recurse -Force }
New-Item -ItemType Directory -Force -Path $Dest | Out-Null

$out = [System.IO.File]::Create($dat)
try {
  $out.Write([byte[]](0x54, 0x48, 0x44, 0x41), 0, 4)
  Write-U16 $out 1
  Write-U16 $out 0
  Write-U32 $out ([long]$sorted.Count)
  foreach ($entry in $sorted) {
    $nameBytes = $utf8.GetBytes([string]$entry.Rel)
    Write-U32 $out ([long]$entry.Offset)
    Write-U32 $out ([long]$entry.Size)
    Write-U16 $out $nameBytes.Length
    $out.Write($nameBytes, 0, $nameBytes.Length)
  }
  if ($out.Position -ne $dataStart) { throw "Archive index size mismatch." }
  foreach ($entry in $sorted) {
    if ($null -ne $entry.Bytes) {
      $out.Write($entry.Bytes, 0, $entry.Bytes.Length)
    }
    else {
      $input = [System.IO.File]::OpenRead([string]$entry.Full)
      try { $input.CopyTo($out) } finally { $input.Dispose() }
    }
  }
}
finally {
  $out.Dispose()
}

foreach ($root in $audioRoots) {
  $from = Join-Path $Repo $root
  Get-ChildItem -LiteralPath $from -Recurse -File | ForEach-Object {
    $rel = $_.FullName.Substring($Repo.Length).TrimStart('\', '/').Replace('\', '/')
    $target = Join-Path $Dest ($rel.Replace('/', '\'))
    $dir = Split-Path $target -Parent
    if (-not (Test-Path -LiteralPath $dir)) { New-Item -ItemType Directory -Force -Path $dir | Out-Null }
    Copy-Item -LiteralPath $_.FullName -Destination $target -Force
  }
}

$check = [System.IO.File]::OpenRead($dat)
try {
  $magic = New-Object byte[] 4
  [void]$check.Read($magic, 0, 4)
  if ($magic[0] -ne 0x54 -or $magic[1] -ne 0x48 -or $magic[2] -ne 0x44 -or $magic[3] -ne 0x41) {
    throw "Packed archive is missing the THDAT header."
  }
}
finally {
  $check.Dispose()
}

Write-Host "Packed $($sorted.Count) files into thsss.dat; left BGM and Sound as wav files"
