# Repository structure

## Source tree layout

```
NuciGenerators.Text.MarkovChain.slnx
README.md
LICENSE
NuciGenerators.Text.MarkovChain/
    MarkovNameGenerator.cs
    NuciGenerators.Text.MarkovChain.csproj
    Models/
        MarkovModel.cs
NuciGenerators.Text.MarkovChain.UnitTests/
    MarkovNameGeneratorTests.cs
    NuciGenerators.Text.MarkovChain.UnitTests.csproj
```

## Module organisation

### NuciGenerators.Text.MarkovChain (main library)

- **Root namespace**: `NuciGenerators.Text.MarkovChain`
- **Public API**: `MarkovNameGenerator`
- **Internal models**: `Models/MarkovModel.cs`

### NuciGenerators.Text.MarkovChain.UnitTests (test project)

- **Root namespace**: `NuciGenerators.Text.MarkovChain.UnitTests`
- **Test class**: `MarkovNameGeneratorTests`
- **Framework**: NUnit 4.x

## Project dependencies

- Main project references:
  - `NuciExtensions` (v5.3.1) — extension methods for collections, strings, etc.
  - `NuciGenerators.Text` (v1.0.1) — base `NameGenerator` class and `Word`/`Wordlist` models
- Test project references:
  - Main project (project reference)
  - `Microsoft.NET.Test.Sdk` (v17.14.1)
  - `NUnit` (v4.3.2)
  - `NUnit3TestAdapter` (v5.0.0)

## Build outputs

- `bin/Debug/net10.0/` — debug builds
- `obj/Debug/net10.0/` — intermediate build artifacts