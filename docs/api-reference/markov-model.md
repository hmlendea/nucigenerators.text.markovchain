# MarkovModel

Base path: `NuciGenerators.Text.MarkovChain.Models.MarkovModel`

## Constructor

```csharp
public MarkovModel(IEnumerable<string> data, int order, float prior, IEnumerable<string> alphabet)
```

### Parameters

| Name | Type | Required | Description |
|------|------|----------|-------------|
| `data` | `IEnumerable<string>` | Yes | Training word values |
| `order` | `int` | Yes | N-gram length (context size), must be >= 1 |
| `prior` | `float` | Yes | Additive smoothing weight, must be >= 0 |
| `alphabet` | `IEnumerable<string>` | Yes | All possible characters including "#" boundary marker |

### Behaviour

1. Stores parameters
2. Initializes `observations` dictionary and `Random`
3. Calls `Train(data)` → builds observation counts
4. Calls `BuildChains()` → converts to probability weights

### Side effects

- Allocates dictionaries and trains model
- Creates `Random` instance

---

## Generate(string context)

```csharp
public string Generate(string context)
```

### Parameters

| Name | Type | Required | Description |
|------|------|----------|-------------|
| `context` | `string` | Yes | Context string of length `order` |

### Returns

`string` — next character from alphabet, or `null` if context not found

### Behaviour

1. Looks up `context` in `chains` dictionary
2. If not found: returns `null`
3. If found: calls `SelectIndex(chain)` to sample from weight distribution
3. Returns `alphabet[index]`

### Side effects

None (read-only).

---

## Train(IEnumerable<string> data) (private)

```csharp
void Train(IEnumerable<string> data)
```

### Behaviour

For each word in data:
1. Wraps: `wrapped = "#".Repeat(order) + word + "#"`
2. Slides window of size `order` across `wrapped`
3. For each position `i`: `context = wrapped.Substring(i, order)`, `nextChar = wrapped[i + order]`
4. Appends `nextChar` to `observations[context]`

### Side effects

Populates `observations` dictionary.

---

## BuildChains() (private)

```csharp
void BuildChains()
```

### Behaviour

For each context in `observations.Keys`:
1. For each `prediction` in `alphabet`:
   - `matches = CountMatches(observations[context], prediction)`
   - `weight = prior + matches`
   - Appends `weight` to `chains[context]`

Result: `chains[context]` is a list of weights parallel to `alphabet`.

### Side effects

Populates `chains` dictionary.

---

## CountMatches(IEnumerable<string>, string) (private static)

```csharp
static int CountMatches(IEnumerable<string> array, string val)
```

Returns count of elements in `array` equal to `val`. Returns 0 if `array` is null.

---

## SelectIndex(IEnumerable<float>) (private)

```csharp
int SelectIndex(IEnumerable<float> chain)
```

### Behaviour

1. Builds cumulative totals from weights
2. Generates `rand = random.NextDouble() * totalWeight`
3. Returns first index where cumulative total > `rand`
4. Returns 0 if no index found (fallback)

### Side effects

Consumes random number from `Random` instance.

---

## Fields (internal)

| Field | Type | Description |
|-------|------|-------------|
| `order` | `int` | N-gram length |
| `prior` | `float` | Smoothing weight |
| `alphabet` | `List<string>` | All possible characters |
| `observations` | `Dictionary<string, List<string>>` | Context → observed next chars |
| `chains` | `Dictionary<string, List<float>>` | Context → probability weights |
| `random` | `Random` | Random number generator |