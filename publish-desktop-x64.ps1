dotnet restore
dotnet build .\{SOLUTION}.sln -c Release --no-restore --nologo
dotnet publish .\path\to\{PROJECT}.csproj -c Release -r win10-x64 /p:PublishSingleFile=true --no-self-contained --no-restore --no-build