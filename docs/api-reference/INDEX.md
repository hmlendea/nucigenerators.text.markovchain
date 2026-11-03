# API reference index

## Overview

This library provides a Markov-chain-based name generation API for .NET. The API is centred around the `MarkovNameGenerator` class.

## Endpoint summary

| Class | Method | Description |
|-------|--------|-------------|
| `MarkovNameGenerator` | `MarkovNameGenerator(List<Word>, int, float)` | Construct from flat word list |
| `MarkovNameGenerator` | `MarkovNameGenerator(List<Wordlist>, int, float)` | Construct from grouped word lists |
| `MarkovNameGenerator` | `Generate()` | Generate a new name |
| `MarkovNameGenerator` | `GetLetter(string)` | Get next character for a context |
| `MarkovModel` | `MarkovModel(IEnumerable<string>, int, float, IEnumerable<string>)` | Construct single-order model |
| `MarkovModel` | `Generate(string)` | Generate next character for context |

## Authentication / authorisation

Not applicable — this is an in-process library with no authentication.

## Versioning

- Package version: 1.0.1
- Target framework: net10.0
- No API versioning scheme (single assembly)

## Rate limiting / throttling

Not applicable — in-process library.

## Related documentation

- [Components: MarkovNameGenerator](../components/markov-name-generator.md)
- [Flows: Name generation](../flows/name-generation.md)
- [Data model](../data-model.md)
- [Architecture](../architecture.md)