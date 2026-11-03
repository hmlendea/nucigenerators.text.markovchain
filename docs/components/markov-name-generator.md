# Components

## MarkovNameGenerator

### Location

`NuciGenerators.Text.MarkovChain/MarkovNameGenerator.cs`

### Responsibility

Public API for Markov-chain-based name generation. Orchestrates training, model creation, and generation with fallback.

### Public API

```csharp
public class MarkovNameGenerator : NameGenerator
{
    public int Order { get; private set; }
    public float Prior { get; private set; }
    public List<Word> Data { get; private set; }

    public MarkovNameGenerator(List<Word> data, int order, float prior)
    public MarkovNameGenerator(List<Wordlist> data, int order, float prior)

    public string Generate()
    public string GetLetter(string context)
}
```

### Construction

Two constructors:
1. `MarkovNameGenerator(List<Word> data, int order, float prior)` — wraps single Word list into Wordlist
2. `MarkovNameGenerator(List<Wordlist> data, int order, float prior)` — merges all Wordlists

Both:
- Store `Order`, `Prior`, merged `Data`
- Extract unique characters from all word values → alphabet
- Prepend "#" boundary marker to alphabet
- Create `Order` MarkovModel instances with decreasing order (Order, Order-1, ..., 1)

### Generation algorithm

`Generate()`:
1. Start with context = "#".Repeat(Order)
2. Loop: call `GetLetter(context)` to get next character
3. If letter is "#" → terminate, return accumulated word
4. If letter is not null → append to word
5. Update context = full word so far
6. Repeat

`GetLetter(context)`:
1. Take last `Order` characters of context as `myContext`
2. For each model in `models` (highest to lowest order):
   - Call `model.Generate(myContext)`
   - If result not null → return it
   - Else shorten `myContext` by removing first character (backoff)
3. Return null if all models fail

### Validation

`GenerationAlgorithm()` (overrides base `NameGenerator`):
1. Calls `Generate()`
2. Removes "#" markers
3. Validates via `IsNameValid(name)` (from base class)
4. Ensures name not in training data (`!Data.Any(w => w.Values.Contains(name))`)
5. Returns name or null

### Dependencies

- `NameGenerator` (base class from NuciGenerators.Text)
- `MarkovModel[]` (composition)
- `NuciExtensions` (extension methods: `Repeat`, `NotEquals`, `TryGetValue`, `AddOrUpdate`, `Pop`, `IsEmpty`)

---

## MarkovModel

### Location

`NuciGenerators.Text.MarkovChain/Models/MarkovModel.cs`

### Responsibility

Single-order Markov chain with probability distributions for character transitions.

### Public API

```csharp
public class MarkovModel
{
    public MarkovModel(IEnumerable<string> data, int order, float prior, IEnumerable<string> alphabet)
    public string Generate(string context)
}
```

### Construction

Parameters:
- `data` — training word values (strings)
- `order` — n-gram length
- `prior` — additive smoothing weight
- `alphabet` — all possible characters including "#"

Process:
1. Store parameters
2. Initialize `observations` dictionary, `Random`
3. Call `Train(data)` → builds observations
4. Call `BuildChains()` → converts observations to probability weights

### Training (`Train`)

For each word in data:
1. Wrap with boundary markers: `"#".Repeat(order) + word + "#"`
2. Slide window of size `order` across wrapped word
3. For each position: context = substring(i, order), nextChar = char at i+order
4. Append nextChar to `observations[context]`

### Chain building (`BuildChains`)

For each context in observations:
1. For each character in alphabet:
   - Count matches of character in observations[context]
   - Weight = `prior + matchesCount`
   - Append to `chains[context]`

Result: `chains[context]` is a list of weights parallel to `alphabet`.

### Generation (`Generate`)

1. Look up `context` in `chains`
2. If not found → return null (triggers fallback in caller)
3. If found → sample index from weight distribution via `SelectIndex`
4. Return `alphabet[index]`

### Sampling (`SelectIndex`)

1. Build cumulative totals from weights
2. Generate random value in [0, totalWeight)
3. Return first index where cumulative total exceeds random value

### Dependencies

- `NuciExtensions` (TryGetValue, AddOrUpdate, Pop, IsEmpty, Repeat)
- `System.Random`
- `System.Collections.Generic`
- `System.Linq`