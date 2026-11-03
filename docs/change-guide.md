# Change guide

## Common modifications

### Adding a new constructor overload

1. Add to `MarkovNameGenerator.cs`
2. Ensure it delegates to existing constructor or replicates initialization logic
3. Update `Data` property assignment
4. Add tests in `MarkovNameGeneratorTests.cs`

### Changing the alphabet construction

Location: `MarkovNameGenerator` constructor, lines 30-45

Current: Extracts characters from word `Values`, sorts, distincts, prepends "#"

To modify: Change character extraction logic, sorting, or boundary marker handling

### Modifying the fallback behavior

Location: `MarkovNameGenerator.GetLetter()`, lines 88-105

Current: Tries each model, shortens context by 1 char on failure

To modify: Change backoff strategy (e.g., skip to next model immediately, different shortening)

### Changing smoothing/prior application

Location: `MarkovModel.BuildChains()`, lines 75-90

Current: `weight = prior + matchesCount` for each alphabet character

To modify: Use different smoothing formula (e.g., Good-Turing, Kneser-Ney)

### Adding validation

Current: No parameter validation in constructors

To add: Validate `order >= 1`, `prior >= 0`, `data != null` at construction start

### Adding serialization support

Requirements:
- Remove `Random` field or make it serializable
- Add `[Serializable]` or use `System.Text.Json` attributes
- Implement `ISerializable` or custom serialization

## Safe modification patterns

1. **Never change public API** without version bump
2. **Add tests first** for new behavior
3. **Preserve invariants** (see `invariants.md`)
4. **Update documentation** in `docs/` for any behavioral change

## Risk areas

| Area | Risk | Mitigation |
|------|------|------------|
| `GetLetter` backoff logic | Infinite loops, wrong context | Add max iteration guard, test edge cases |
| `MarkovModel` training | Memory blowup with large data | Monitor `observations`/`chains` size |
| `Random` usage | Non-determinism, thread safety | Consider `ThreadLocal<Random>` or injected RNG |
| Base class `NameGenerator` | Hidden behavior changes | Pin `NuciGenerators.Text` version, test upgrades |