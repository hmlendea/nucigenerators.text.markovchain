# Testing

## Test strategy

- Unit tests for `MarkovNameGenerator` and `MarkovModel` behavior
- Parameterized tests for different configurations
- Deterministic tests using fixed training data
- No integration tests (no external dependencies)

## Test organisation

```
NuciGenerators.Text.MarkovChain.UnitTests/
    MarkovNameGeneratorTests.cs
```

- Single test class: `MarkovNameGeneratorTests`
- Framework: NUnit 4.x
- Test naming: `Given[Condition]_When[Action]_Then[Outcome]`

## Coverage areas

### MarkovNameGenerator

| Test | Description |
|------|-------------|
| `GivenTrainingWords_WhenConstructed_ThenConfigurationAndDataAreRetained` | Verifies constructor stores Order, Prior, Data correctly |
| `GivenAUniqueTrainingWordAndNoPrior_WhenGenerating_ThenTheTrainingWordIsReproduced` | With order=1, prior=0, single training word "Bobert" → generates "#Bobert" |
| `GivenAUniqueTrainingWordAndNoPrior_WhenGettingALetter_ThenTheExpectedTransitionIsReturned` | Tests GetLetter transitions: "#"→"B", "t"→"#" |

### MarkovModel

No direct unit tests (tested indirectly through MarkovNameGenerator).

## Test data

```csharp
private static readonly List<Word> trainingWords =
[
    new Word { Id = "bobert", Values = ["Bobert"] }
];
```

Single training word "Bobert" used for deterministic behavior verification.

## Running tests

```bash
dotnet test NuciGenerators.Text.MarkovChain.UnitTests/NuciGenerators.Text.MarkovChain.UnitTests.csproj
```

## Test constraints

- Cross-platform: no OS-specific APIs, paths, or locale dependencies
- Deterministic: fixed training data, controlled randomness (prior=0 for exact reproduction)
- No `InternalsVisibleTo` — tests only public API
- No reflection to access non-public members

## Assertions

- NUnit constraint model: `Assert.That(actual, Is.EqualTo(expected))`
- No `Assert.AreEqual`, `Assert.IsTrue`, `Assert.IsNotNull`
- Collection assertions: `Has.Count.EqualTo(n)`, `Is.EqualTo(collection)`

## Known gaps

- No tests for:
  - Higher order (>1) behavior
  - Prior > 0 smoothing effects
  - Fallback/backoff behavior
  - Multiple training words
  - Wordlist constructor overload
  - GenerationAlgorithm validation (IsNameValid, duplicate detection)
  - Edge cases: empty data, invalid order/prior values