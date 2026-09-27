Write-Host "`nDeploying CLI app" -ForegroundColor Cyan

$cliTarget = "$env:APPDATA\utils\reddit.exe"
New-Item -ItemType Directory -Force -Path (Split-Path $cliTarget) | Out-Null
Copy-Item -Path .\pubcli\cli.exe -Destination $cliTarget -Force
Write-Host "Installed to $cliTarget" -ForegroundColor Green
