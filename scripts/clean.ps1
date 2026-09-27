Get-ChildItem -Path . -Directory -Recurse -Include bin, obj |
    ForEach-Object {
        Write-Host "Cleaning directory $($_.FullName)"
        Remove-Item $_.FullName -Recurse -Force
    }
