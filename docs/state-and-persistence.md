# State and persistence

## State management

### In-memory state

| Component | State | Lifetime |
|-----------|-------|----------|
| `MarkovNameGenerator.Order` | Configuration | Instance lifetime |
| `MarkovNameGenerator.Prior` | Configuration | Instance lifetime |
| `MarkovNameGenerator.Data` | Training data | Instance lifetime |
| `MarkovNameGenerator.models` | Trained models | Instance lifetime |
| `MarkovModel.observations` | Training observations | Instance lifetime |
| `MarkovModel.chains` | Probability chains | Instance lifetime |
| `MarkovModel.random` | RNG state | Instance lifetime |

### No persistent state

- No database, no files, no cache
- No background processes
- No singleton or static state (except `Random` default seed)

## Persistence

### Not supported

- `MarkovNameGenerator` not serializable
- `MarkovModel` not serializable
- Contains `Random` (non-serializable)
- No `ISerializable` implementation
- No JSON serialization attributes

### Workaround

Retrain on startup:
```csharp
// Save training data (Word/Wordlist) separately
// Reconstruct generator on load:
var generator = new MarkovNameGenerator(savedWords, order, prior);
```

## Caches

No caching layer. All computation in constructor.

## Migrations

Not applicable — no persisted schema.