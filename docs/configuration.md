# Configuration

## Configuration sources

This library has no external configuration files, environment variables, or configuration providers. All configuration is passed programmatically via constructor parameters.

## Constructor parameters

### MarkovNameGenerator

| Parameter | Type | Constraints | Description |
|-----------|------|-------------|-------------|
| `data` | `List<Word>` or `List<Wordlist>` | Non-null, non-empty | Training data |
| `order` | `int` | >= 1 | Markov order (n-gram length) |
| `prior` | `float` | >= 0 | Additive smoothing weight |

### MarkovModel

| Parameter | Type | Constraints | Description |
|-----------|------|-------------|-------------|
| `data` | `IEnumerable<string>` | Non-null | Training word values |
| `order` | `int` | >= 1 | N-gram length |
| `prior` | `float` | >= 0 | Smoothing weight |
| `alphabet` | `IEnumerable<string>` | Non-null, contains "#" | Character set including boundary marker |

## Parameter tuning guidance

| Parameter | Effect of increasing | Effect of decreasing | Typical range |
|-----------|---------------------|---------------------|---------------|
| `order` | Outputs closer to training data, less creative | More variation, more novelty | 2-3 |
| `prior` | Flatter probabilities, less overfitting | Sharper probabilities, more overfitting | 0.2-1.0 |

## Precedence

Not applicable — no layered configuration.

## Runtime modification

Configuration is immutable after construction. To change parameters, create a new instance.