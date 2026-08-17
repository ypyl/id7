# Publish

Build and pack the tool, then push the package to NuGet.

```powershell
dotnet pack -c Release
dotnet nuget push .\nupkg\id7.<version>.nupkg --source https://api.nuget.org/v3/index.json --api-key $env:NUGET_API_KEY
```

Never commit the API key. Set `NUGET_API_KEY` in your shell or CI secret store before publishing.