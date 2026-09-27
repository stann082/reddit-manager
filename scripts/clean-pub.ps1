param([string[]]$Directories = @('pubcli', 'pubweb'))

foreach ($dir in $Directories) {
    if (Test-Path $dir) {
        Write-Host "Cleaning directory $dir"
        Remove-Item $dir -Recurse -Force
    }
}
