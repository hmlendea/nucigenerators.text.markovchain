# Data model

## Domain entities

### Word (from NuciGenerators.Text.Models)

```csharp
public class Word
{
    public string Id { get; set; }
    public List<string> Values { get; set; }
}
```

- **Id**: Unique identifier for the word entry
- **Values**: List of string variants (e.g., different capitalizations, translations)

### Wordlist (from NuciGenerators.Text.Models)

```csharp
public class Wordlist : List<Word>
{
}
```

- A collection of `Word` objects
- Used for grouped training data (e.g., by language, category)

## Internal models

### MarkovModel

```csharp
public class MarkovModel
{
    readonly int order;
    readonly float prior;
    readonly List<string> alphabet;
    readonly Dictionary<string, List<string>> observations;
    Dictionary<string, List<float>> chains;
    readonly Random random;
}
```

| Field | Type | Description |
|-------|------|-------------|
| `order` | `int` | N-gram length (context size) |
| `prior` | `float` | Additive smoothing weight |
| `alphabet` | `List<string>` | All possible characters including "#" boundary marker |
| `observations` | `Dictionary<string, List<string>>` | Context → list of observed next characters |
| `chains` | `Dictionary<string, List<float>>` | Context → probability weights per alphabet character |
| `random` | `Random` | Random number generator for sampling |

### MarkovNameGenerator

```csharp
public class MarkovNameGenerator : NameGenerator
{
    public int Order { get; private set; }
    public float Prior { get; private set; }
    public List<Word> Data { get; private set; }
    readonly List<MarkovModel> models;
}
```

| Property/Field | Type | Description |
|----------------|------|-------------|
| `Order` | `int` | Markov order (n-gram length) |
| `Prior` | `float` | Smoothing factor |
| `Data` | `List<Word>` | Merged training data from all Wordlists |
| `models` | `List<MarkovModel>` | Chain of models from highest to lowest order |

## Relationships

```
MarkovNameGenerator (1) ───► (Order) MarkovModel
MarkovNameGenerator (1) ───► (N) Word (Data)
MarkovNameGenerator ───► NameGenerator (inheritance)
```

## Persistence

No built-in persistence. Training data is provided in-memory via constructor. Serialization of models is not implemented.

## Serialisation

- `Word` and `Wordlist` from NuciGenerators.Text support standard .NET serialization
- `MarkovModel` and `MarkovNameGenerator` are not serializable (contain `Random`, dictionaries with no serialization attributes)