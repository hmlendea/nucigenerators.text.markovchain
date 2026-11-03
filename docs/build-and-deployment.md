# Build and deployment

## Build pipeline

### Local build

```bash
# Build solution
dotnet build NuciGenerators.Text.MarkovChain.slnx

# Build specific project
dotnet build NuciGenerators.Text.MarkovChain/NuciGenerators.Text.MarkovChain.csproj
```

### Release build

```bash
dotnet build -c Release NuciGenerators.Text.MarkovChain.slnx
```

### Pack (NuGet package)

```bash
dotnet pack -c Release NuciGenerators.Text.MarkovChain/NuciGenerators.Text.MarkovChain.csproj
```

Output: `NuciGenerators.Text.MarkovChain/bin/Release/NuciGenerators.Text.MarkovChain.1.0.1.nupkg`

## CI/CD

### GitHub Actions

Workflow: `.github/workflows/dotnet.yml`

Triggers:
- Push to main/master
- Pull requests

Steps:
1. Checkout
2. Setup .NET 10.0
3. Restore dependencies
4. Build
5. Test
6. Pack (on tag push)

### NuGet publishing

Manual or via GitHub Actions on version tag:
```bash
dotnet nuget push NuciGenerators.Text.MarkovChain/bin/Release/*.nupkg --api-key $NUGET_API_KEY --source https://api.nuget.org/v3/index.json
```

## Environments

- Development: Local .NET 10.0 SDK
- CI: GitHub Actions ubuntu-latest with .NET 10.0
- Production: NuGet.org package consumers

## Artifacts

- NuGet package: `NuciGenerators.Text.MarkovChain.{version}.nupkg`
- Symbols package: `NuciGenerators.Text.MarkovChain.{version}.snupkg` (if enabled)

## Versioning

- Semantic versioning in `.csproj`: `<Version>1.0.1</Version>`
- Package version = assembly version
- Update version in `.csproj` before release