# Architecture

## High-level decomposition

```
┌─────────────────────────────────────────────────────────────┐
│                    MarkovNameGenerator                      │
│  (public API, inherits from NameGenerator)                  │
└─────────────────────────────────────────────────────────────┘
                              │
                              ▼
┌─────────────────────────────────────────────────────────────┐
│                      MarkovModel[]                          │
│  (one per order level: Order, Order-1, ..., 1)             │
└─────────────────────────────────────────────────────────────┘
```

## Component responsibilities

### MarkovNameGenerator

- **Purpose**: Public entry point for name generation
- **Owns**: Configuration (Order, Prior), training data (Data), array of MarkovModel instances
- **Does not own**: Persistence, I/O, external services
- **Entry**: Constructed with training data, order, and prior; `Generate()` invoked by consumers
- **Execution**: Builds domain alphabet, creates model chain, generates via fallback loop

### MarkovModel

- **Purpose**: Represents a single-order Markov chain with probability distributions
- **Owns**: Observations dictionary, chains dictionary, alphabet, random generator
- **Does not own**: Training data (receives it in constructor), fallback logic
- **Entry**: `Generate(context)` called by MarkovNameGenerator.GetLetter()
- **Execution**: Looks up context in chains, samples from probability distribution

## Dependency direction

```
MarkovNameGenerator → MarkovModel[] (composition)
MarkovNameGenerator → NameGenerator (inheritance, from NuciGenerators.Text)
MarkovNameGenerator → NuciExtensions (extension methods)
MarkovModel → NuciExtensions (extension methods)
MarkovModel → System.Random
```

## Runtime topology

Single-threaded, in-process library. No external services, no async operations, no background threads.

## Major state

- `MarkovNameGenerator.Order` — n-gram length (immutable after construction)
- `MarkovNameGenerator.Prior` — smoothing weight (immutable after construction)
- `MarkovNameGenerator.Data` — merged training words (immutable after construction)
- `MarkovNameGenerator.models` — array of MarkovModel (immutable after construction)
- `MarkovModel.chains` — context → probability distribution (built once in constructor)
- `MarkovModel.observations` — context → observed next characters (built once, used to build chains)

## System-wide invariants

1. `Order >= 1`
2. `Prior >= 0`
3. `models.Length == Order`
4. `models[i].order == Order - i` for all i
5. All models share the same alphabet (domain + "#")
6. Generation always terminates (eventually produces "#" boundary marker)
7. `GenerationAlgorithm()` returns null if generated name exists in training data or fails validation