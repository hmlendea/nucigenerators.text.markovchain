[![Donate](https://img.shields.io/badge/-%E2%99%A5%20Donate-%23ff69b4)](https://hmlendea.go.ro/fund.html)
[![Latest Release](https://img.shields.io/github/v/release/hmlendea/nucigenerators.text.markovchain)](https://github.com/hmlendea/nucigenerators.text.markovchain/releases/latest)
[![Build Status](https://github.com/hmlendea/nucigenerators.text.markovchain/actions/workflows/dotnet.yml/badge.svg)](https://github.com/hmlendea/nucigenerators.text.markovchain/actions/workflows/dotnet.yml)
[![License: GPL v3](https://img.shields.io/badge/License-GPLv3-blue.svg)](https://gnu.org/licenses/gpl-3.0)

# NuciGenerators.Text.MarkovChain

Markov-chain-based name generation for .NET.

This library learns character transitions from training words, then generates new words that statistically resemble the original dataset.

[![Get it from NuGet](https://raw.githubusercontent.com/hmlendea/readme-assets/master/badges/stores/nuget.png)](https://nuget.org/packages/NuciGenerators.Text.MarkovChain)

## Features

- Character-level Markov generation
- Configurable Markov order (`n`-gram length)
- Smoothing with configurable prior weight
- Fallback from higher-order to lower-order models for robust generation

## Requirements

- .NET target framework: `net10.0`

## Installation

### .NET CLI

```bash
dotnet add package NuciGenerators.Text.MarkovChain
```

### Package Manager

```powershell
Install-Package NuciGenerators.Text.MarkovChain
```

## Quick Start

```csharp
using NuciGenerators.Text.MarkovChain;
using NuciGenerators.Text.Models;

// Assume trainingWords is a List<Word> loaded from your own source.
List<Word> trainingWords = LoadTrainingWords();

// order: context length in characters (higher = stricter, lower = more creative)
// prior: smoothing factor (higher = flatter probabilities)
var generator = new MarkovNameGenerator(trainingWords, order: 3, prior: 0.5f);

string generated = generator.Generate();
Console.WriteLine(generated);
```

## API Overview

### `MarkovNameGenerator`

- Constructor: `MarkovNameGenerator(List<Word> data, int order, float prior)`
- Constructor: `MarkovNameGenerator(List<Wordlist> data, int order, float prior)`
- Method: `Generate()`
	- Generates a result based on learned character transitions.

## How It Works

1. Training words are transformed into character sequences.
2. Boundary markers (`#`) are added to represent start/end of words.
3. Transition counts are collected for each context of length `order`.
4. Probabilities are built with additive smoothing via `prior`.
5. During generation, the model backs off to lower-order chains when a context is unseen.

## Parameter Tuning

- Increase `order` for outputs closer to the source data.
- Decrease `order` for more variation and novelty.
- Increase `prior` to reduce overfitting to rare transitions.
- Decrease `prior` to emphasize observed transitions.

Typical starting values:

- `order = 2` or `3`
- `prior = 0.2f` to `1.0f`

## Development

### Build

```bash
dotnet build NuciGenerators.Text.MarkovChain.sln
```

### Pack

```bash
dotnet pack -c Release
```

## Contributing

Contributions are welcome.

Please:

- keep changes cross-platform
- preserve public APIs unless the change is intentionally breaking
- keep pull requests focused and consistent with existing style
- update documentation when behaviour changes
- add or update tests for new behaviour

## Related Projects

- [NuciGenerators.Text](https://github.com/hmlendea/nucigenerators.text)
- [NuciGenerators.Text.MarkovChain](https://github.com/hmlendea/nucigenerators.text.markovchain)

## License

Licensed under the GNU General Public License v3.0 or later.
See [LICENSE](./LICENSE) for details.