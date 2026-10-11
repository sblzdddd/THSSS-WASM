param(
  [Parameter(Mandatory = $true)][string]$Repo,
  [Parameter(Mandatory = $true)][string]$Dest,
  [switch]$SkipCopy
)
$ErrorActionPreference = "Stop"
$Repo = (Resolve-Path -LiteralPath $Repo).Path.TrimEnd('\', '/')
$required = @("Image", "CS", "FX", "Model", "Story", "Sound", "BGM")
$optional = @("Replay")

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

if ($SkipCopy) {
  Write-Host "Game content validated; staged assets already present."
  return
}

if (Test-Path -LiteralPath $Dest) { Remove-Item -LiteralPath $Dest -Recurse -Force }
New-Item -ItemType Directory -Force -Path $Dest | Out-Null
$lines = New-Object System.Collections.Generic.List[string]
foreach ($root in ($required + $optional)) {
  $from = Join-Path $Repo $root
  if (-not (Test-Path -LiteralPath $from -PathType Container)) { continue }
  Get-ChildItem -LiteralPath $from -Recurse -File | ForEach-Object {
    $rel = $_.FullName.Substring($Repo.Length).TrimStart('\', '/').Replace('\', '/')
    $target = Join-Path $Dest ($rel.Replace('/', '\'))
    $dir = Split-Path $target -Parent
    if (-not (Test-Path -LiteralPath $dir)) { New-Item -ItemType Directory -Force -Path $dir | Out-Null }
    Copy-Item -LiteralPath $_.FullName -Destination $target -Force
    $lines.Add($rel)
  }
}
$score = Join-Path $Repo "Score.dat"
if (Test-Path -LiteralPath $score -PathType Leaf) {
  Copy-Item -LiteralPath $score -Destination (Join-Path $Dest "Score.dat") -Force
  $lines.Add("Score.dat")
}
if ($lines.Count -eq 0) {
  throw "Game content check failed: no files were staged. Restore the gitignored asset folders at the repo root, or build with -p:StageGameAssets=false."
}
$manifest = Join-Path $Dest "manifest.txt"
[System.IO.File]::WriteAllLines($manifest, $lines, [System.Text.UTF8Encoding]::new($false))
Write-Host "Staged $($lines.Count) content files"
