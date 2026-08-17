# Publish

Releases push to NuGet through Trusted Publishing (OIDC). No global API key is stored in the repo or in GitHub secrets. The publish workflow exchanges a short-lived OIDC token for a temporary NuGet API key.

## One-time setup

1. On NuGet.org, register the trusted publisher for this repository:
   - Account: your NuGet.org account
   - Trusted publisher: GitHub Actions with Owner `ypyl`, Repository `id7`, Workflow `publish.yml`
2. Add the `NUGET_USERNAME` secret to this GitHub repository. Value: your NuGet.org username (not a password or key).

Reference: https://learn.microsoft.com/en-us/nuget/nuget-org/trusted-publishing

## Publishing a release

1. Tag the release and push the tag:

   ```powershell
   git tag v1.0.4
   git push origin v1.0.4
   ```

2. The `Publish` workflow builds, packs, and pushes the `.nupkg` to NuGet automatically. The package version is taken from the tag.

## Local pack for testing

```powershell
dotnet pack -c Release -o ./artifacts
```