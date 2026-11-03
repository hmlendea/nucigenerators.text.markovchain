# Dependencies

## External dependencies (NuGet)

### NuciGenerators.Text.MarkovChain (main project)

| Package | Version | Purpose |
|---------|---------|---------|
| `NuciExtensions` | 5.3.1 | Extension methods: `Repeat`, `NotEquals`, `TryGetValue`, `AddOrUpdate`, `Pop`, `IsEmpty` |
| `NuciGenerators.Text` | 1.0.1 | Base `NameGenerator` class, `Word`, `Wordlist` models |

### NuciGenerators.Text.MarkovChain.UnitTests (test project)

| Package | Version | Purpose |
|---------|---------|---------|
| `Microsoft.NET.Test.Sdk` | 17.14.1 | Test runner infrastructure |
| `NUnit` | 4.3.2 | Test framework |
| `NUnit3TestAdapter` | 5.0.0 | NUnit adapter for dotnet test |

## Internal dependencies

```
NuciGenerators.Text.MarkovChain.UnitTests
    └── NuciGenerators.Text.MarkovChain (project reference)
```

## Framework dependencies

- .NET 10.0 (`net10.0`)
- `System.Collections.Generic`
- `System.Linq`
- `System.Random`

## Transitive dependencies

`NuciExtensions` and `NuciGenerators.Text` may have their own dependencies. Check their respective repositories for details.

## Version constraints

- All projects target `net10.0`
- Package references use exact versions (no floating versions)
- `NuciGenerators.Text` version matches this package version (1.0.1)