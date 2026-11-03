# Repository overview

## Purpose

NuciGenerators.Text.MarkovChain is a .NET library that implements Markov-chain-based name generation. It learns character-level transition probabilities from a training dataset of words, then generates new words that statistically resemble the original data.

## Scope

The library provides:

- Character-level Markov chain generation with configurable order (n-gram length)
- Additive smoothing via a configurable prior weight
- Fallback from higher-order to lower-order models for robust generation when contexts are unseen
- Integration with the NuciGenerators.Text ecosystem via the `NameGenerator` base class

## Entry points

- `MarkovNameGenerator` — main public class for generating names
- `MarkovModel` — internal model representing a single-order Markov chain

## Target framework

- .NET 10.0 (`net10.0`)

## License

GPL-3.0-or-later