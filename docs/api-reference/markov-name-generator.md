# MarkovNameGenerator

Base path: `NuciGenerators.Text.MarkovChain.MarkovNameGenerator`

## Constructor (List<Word>, int, float)

```csharp
public MarkovNameGenerator(List<Word> data, int order, float prior)
```

### Parameters

| Name | Type | Required | Description |
|------|------|----------|-------------|
| `data` | `List<Word>` | Yes | Training words |
| `order` | `int` | Yes | Markov order (n-gram length), must be >= 1 |
| `prior` | `float` | Yes | Additive smoothing weight, must be >= 0 |

### Behaviour

Wraps `data` into a single `Wordlist` and delegates to the `List<Wordlist>` constructor.

### Side effects

- Builds alphabet from training data
- Creates `Order` MarkovModel instances
- Trains all models

---

## Constructor (List<Wordlist>, int, float)

```csharp
public MarkovNameGenerator(List<Wordlist> data, int order, float prior) : base(data)
```

### Parameters

| Name | Type | Required | Description |
|------|------|----------|-------------|
| `data` | `List<Wordlist>` | Yes | Grouped training word lists |
| `order` | `int` | Yes | Markov order (n-gram length), must be >= 1 |
| `prior` | `float` | Yes | Additive smoothing weight, must be >= 0 |

### Behaviour

1. Merges all Wordlists into flat `List<Word>` stored in `Data`
2. Extracts unique characters from all word `Values` → alphabet
3. Prepends "#" boundary marker to alphabet
4. Creates `Order` MarkovModel instances with orders: `Order`, `Order-1`, ..., `1`
5. Each model trained on same word values with shared alphabet and prior

### Side effects

- Allocates and trains `Order` MarkovModel instances
- Stores configuration in `Order`, `Prior`, `Data` properties

---

## Generate()

```csharp
public string Generate()
```

### Returns

`string` — generated word including "#" boundary markers, or empty string if generation fails

### Behaviour

1. Initializes `word = "#".Repeat(Order)`
2. Calls `GetLetter(word)` to get first character
3. While letter != "#":
   - If letter not null: appends to `word`
   - Calls `GetLetter(word)` for next character
4. Returns accumulated `word` (includes "#" markers)

### Side effects

None (read-only operation on models).

### Idempotency

Not idempotent — each call produces a new random result.

---

## GetLetter(string context)

```csharp
public string GetLetter(string context)
```

### Parameters

| Name | Type | Required | Description |
|------|------|----------|-------------|
| `context` | `string` | Yes | Current generated word (including "#" prefix) |

### Returns

`string` — next character, or `null` if all models fail

### Behaviour

1. Extracts `myContext = context.Substring(context.Length - Order, Order)`
2. For each model in `models` (highest to lowest order):
   - Calls `model.Generate(myContext)`
   - If result not null: returns it
   - Else: shortens `myContext = myContext[1..]` (backoff)
3. Returns `null` if all models fail

### Side effects

None.

---

## GenerationAlgorithm() (protected override)

```csharp
protected override string GenerationAlogrithm()
```

### Returns

`string` — validated generated name without "#" markers, or `null` if invalid

### Behaviour

1. Calls `Generate()`
2. Removes all "#" characters
3. Validates via base `IsNameValid(name)`
4. Checks name not in training data: `!Data.Any(w => w.Values.Contains(name))`
5. Returns name if valid and novel, else `null`

### Side effects

None.

### Flow references

- [Name generation flow](../flows/name-generation.md)

---

## Properties

### Order

```csharp
public int Order { get; private set; }
```

Markov order (n-gram length). Immutable after construction.

### Prior

```csharp
public float Prior { get; private set; }
```

Additive smoothing weight. Immutable after construction.

### Data

```csharp
public List<Word> Data { get; private set; }
```

Merged training words from all Wordlists. Immutable after construction.