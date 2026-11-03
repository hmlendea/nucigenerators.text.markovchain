# Ambiguities and open questions

## Known ambiguities

### NameGenerator base class behavior

- `IsNameValid()` implementation not visible (in NuciGenerators.Text)
- `GenerationAlgorithm()` override expected but base contract unclear
- `GenerateName()` method on base class not inspected

### Word/Wordlist models

- From NuciGenerators.Text.Models — exact structure assumed from usage
- `Id` field purpose unclear (not used in generation)
- `Values` list — multiple strings per Word: are they variants, translations, or alternatives?

### NuciExtensions dependency

- Extension methods used: `Repeat`, `NotEquals`, `TryGetValue`, `AddOrUpdate`, `Pop`, `IsEmpty`
- Exact behavior not verified (e.g., `TryGetValue` returns null or default?)
- `AddOrUpdate` TODO comment in MarkovModel.Train: "Does the update part cause issues?"

## Open questions

### Algorithm behavior

1. **Backoff strategy**: Current implementation shortens context by 1 character and retries same model. Is this optimal? Should it immediately fall back to next lower-order model?

2. **Boundary marker conflation**: Same "#" used for start and end. Does this cause start/end pattern confusion?

3. **Prior application**: Uniform prior added to all transitions. Would context-dependent prior (e.g., based on context frequency) improve quality?

4. **Order selection**: Models created for Order, Order-1, ..., 1. Why not include order=0 (unigram) as final fallback?

### Testing gaps

1. No tests for order > 1
2. No tests for prior > 0
3. No tests for fallback/backoff behavior
4. No tests for Wordlist constructor
5. No tests for GenerationAlgorithm validation path
5. No tests for edge cases (empty data, invalid parameters)

### Performance concerns

1. `observations` and `chains` dictionaries can grow large with high order and large alphabet
2. `Train` uses `Pop()` on list — modifies input, O(n) per pop
3. `BuildChains` iterates all contexts × alphabet — O(contexts × alphabet)
4. No streaming/incremental training support

### Serialization

1. No serialization support — models must be retrained
2. `Random` instance prevents standard serialization
3. No versioning strategy for persisted models

### Thread safety

1. `Random` not thread-safe — concurrent `Generate()` calls on same instance unsafe
2. No documentation on thread safety guarantees

### API design

1. `Generate()` returns string with "#" markers — caller must strip. Should there be a `GenerateClean()`?
2. `GetLetter()` public but seems internal — why exposed?
3. `Order` and `Prior` public getters but no setters — intentional immutability
4. `Data` property exposes internal list — mutable by caller

## TODOs from source

- `MarkovModel.cs:50`: `// TODO: Does the update part cause issues?` — `AddOrUpdate` in Train
- No other TODO comments found