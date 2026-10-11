param([Parameter(Mandatory = $true)][string]$Repo, [Parameter(Mandatory = $true)][string]$Dest)
$ErrorActionPreference = "Stop"
$Repo = (Resolve-Path $Repo).Path.TrimEnd('\', '/')
$roots = @("Image", "CS", "FX", "Model", "Story", "Replay")
if (Test-Path $Dest) { Remove-Item $Dest -Recurse -Force }
New-Item -ItemType Directory -Force -Path $Dest | Out-Null
$lines = New-Object System.Collections.Generic.List[string]
foreach ($root in $roots) {
  $from = Join-Path $Repo $root
  if (-not (Test-Path $from)) { continue }
  Get-ChildItem $from -Recurse -File | ForEach-Object {
    $rel = $_.FullName.Substring($Repo.Length).TrimStart('\', '/').Replace('\', '/')
    $target = Join-Path $Dest ($rel.Replace('/', '\'))
    $dir = Split-Path $target -Parent
    if (-not (Test-Path $dir)) { New-Item -ItemType Directory -Force -Path $dir | Out-Null }
    Copy-Item $_.FullName $target -Force
    $lines.Add($rel)
  }
}
$score = Join-Path $Repo "Score.dat"
if (Test-Path $score) {
  Copy-Item $score (Join-Path $Dest "Score.dat") -Force
  $lines.Add("Score.dat")
}
$manifest = Join-Path $Dest "manifest.txt"
[System.IO.File]::WriteAllLines($manifest, $lines, [System.Text.UTF8Encoding]::new($false))
Write-Host "Staged $($lines.Count) content files"
