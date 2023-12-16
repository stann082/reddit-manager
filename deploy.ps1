remove-item .\pub\ -recurse -erroraction silentlycontinue
dotnet publish .\src\app\app.csproj -c Release -o pub
copy-item -path .\pub\app.exe -destination $env:APPDATA\utils\reddit.exe -verbose
