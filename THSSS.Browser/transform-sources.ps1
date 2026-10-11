param([Parameter(Mandatory = $true)][string]$Source, [Parameter(Mandatory = $true)][string]$Dest)
$ErrorActionPreference = "Stop"
$Source = (Resolve-Path $Source).Path.TrimEnd('\', '/')
$Dest = [System.IO.Path]::GetFullPath($Dest)
if (Test-Path $Dest) { Remove-Item $Dest -Recurse -Force }
New-Item -ItemType Directory -Force -Path $Dest | Out-Null
$files = Get-ChildItem -Path $Source -Filter *.cs -Recurse | Where-Object {
  $_.FullName -notmatch '\\(bin|obj)\\' -and $_.Name -ne 'Program.cs' -and $_.Name -ne 'AssemblyInfo.cs'
}
foreach ($file in $files) {
  $rel = $file.FullName.Substring($Source.Length).TrimStart('\', '/')
  $out = Join-Path $Dest $rel
  $dir = Split-Path $out -Parent
  if (-not (Test-Path $dir)) { New-Item -ItemType Directory -Force -Path $dir | Out-Null }
  $bytes = [System.IO.File]::ReadAllBytes($file.FullName)
  $list = New-Object System.Collections.Generic.List[byte]
  for ($i = 0; $i -lt $bytes.Length; $i++) {
    if ($i + 1 -lt $bytes.Length -and $bytes[$i] -eq 0x5C -and $bytes[$i + 1] -eq 0x5C) {
      $list.Add(0x2F)
      $i++
    } else {
      $list.Add($bytes[$i])
    }
  }
  $raw = $list.ToArray()
  $hasBom = $raw.Length -ge 3 -and $raw[0] -eq 0xEF -and $raw[1] -eq 0xBB -and $raw[2] -eq 0xBF
  $text = [System.Text.Encoding]::UTF8.GetString($raw)
  $text = $text.Replace('System.Text.Encoding.Default', 'Shooting.Ansi.GBK')
  $text = $text.Replace('Encoding.Default', 'Shooting.Ansi.GBK')
  $text = $text.Replace('.ForEach(', '.ForEachNet48(')
  $utf8 = New-Object System.Text.UTF8Encoding $hasBom
  [System.IO.File]::WriteAllBytes($out, $utf8.GetBytes($text))
}
Write-Host "Transformed $($files.Count) game sources"
