[![Donate](https://img.shields.io/badge/-%E2%99%A5%20Donate-%23ff69b4)](https://hmlendea.go.ro/funding)
[![Latest Release](https://img.shields.io/github/v/release/hmlendea/nucigenerators.text.markovchain)](https://github.com/hmlendea/nucigenerators.text.markovchain/releases/latest)
[![Build Status](https://github.com/hmlendea/nucigenerators.text.markovchain/actions/workflows/dotnet.yml/badge.svg)](https://github.com/hmlendea/nucigenerators.text.markovchain/actions/workflows/dotnet.yml)
[![NuGet](https://img.shields.io/nuget/v/NuciGenerators.Text.MarkovChain)](https://nuget.org/packages/NuciGenerators.Text.MarkovChain)
[![License](https://img.shields.io/github/license/hmlendea/nucigenerators.text.markovchain)](https://github.com/hmlendea/nucigenerators.text.markovchain/blob/master/LICENSE)

# NuciGenerators.Text.MarkovChain

Markov-chain-based name generation library for .NET.

## Table of Contents

- [Capabilities](#capabilities)
- [Usage](#usage)
- [Installation](#installation)
  - [Package Manager Installation](#package-manager-installation)
- [Development](#development)
  - [Requirements](#requirements)
  - [Build](#build)
  - [Test](#test)
- [GitHub Actions](#github-actions)
- [Project Structure](#project-structure)
  - [Projects and Packages](#projects-and-packages)
- [Architecture](#architecture)
- [Documentation](#documentation)
- [FAQ](#faq)
- [Troubleshooting](#troubleshooting)
- [Contributing](#contributing)
- [Code of Conduct](#code-of-conduct)
- [Privacy and Data](#privacy-and-data)
- [Security](#security)
- [Project Engagement](#project-engagement)
- [License](#license)

## ✨ Capabilities

- Character-level Markov generation
- Configurable Markov order (`n`-gram length)
- Smoothing with configurable prior weight
- Fallback from higher-order to lower-order models for robust generation

## 🚀 Usage

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

## 📦 Installation

[![Obtain it from NuGet](https://raw.githubusercontent.com/hmlendea/readme-assets/master/badges/stores/nuget.png)](https://nuget.org/packages/NuciGenerators.Text.MarkovChain)

### Package Manager Installation

```bash
dotnet add package NuciGenerators.Text.MarkovChain
```

Or, via the `Package Manager Console`:
```powershell
Install-Package NuciGenerators.Text.MarkovChain
```

## 🛠️ Development

### Requirements

- [.NET 10.0 SDK](https://dotnet.microsoft.com/download/dotnet/10.0)

### Build

```bash
dotnet build NuciGenerators.Text.MarkovChain.slnx
```

### Test

```bash
dotnet test --no-build --verbosity normal
```

## ⚙️ GitHub Actions

| Workflow | Purpose | What it does |
|----------|---------|--------------|
| [ dotnet.yml ](https://github.com/hmlendea/nucigenerators.text.markovchain/blob/master/.github/workflows/dotnet.yml) | Continuous integration | Restores dependencies, builds the solution, and runs tests on every push and pull request to master |

## 🗂️ Project Structure

### Projects and Packages

| Project | Type | Purpose |
|---------|------|---------|
| `NuciGenerators.Text.MarkovChain` | Library | Markov-chain-based name generation library for .NET |
| `NuciGenerators.Text.MarkovChain.UnitTests` | Unit Tests | Unit tests for the Markov chain name generator |

## 🏗️ Architecture

NuciGenerators.Text.MarkovChain is a .NET library that implements character-level Markov chain generation for name creation. It learns transition probabilities from training data and generates new words that statistically resemble the source dataset.

See the [architecture documentation](./ARCHITECTURE.md) for the system context, principal components, runtime flows, ownership boundaries, dependencies, constraints, and extension points.

## 📚 Documentation

| Resource | Description |
|----------|-------------|
| [Documentation Index](./docs/INDEX.md) | Complete documentation index with links to all guides and references |

## ❓ FAQ

See the [Frequently Asked Questions](./docs/faq.md) for responses to frequently raised inquiries.

## 🩺 Troubleshooting

See [Troubleshooting Guide](./docs/troubleshooting.md) for diagnostic procedures and resolutions to recurring problems.

## 🤝 Contributing

You are welcome to submit any suggestion, feedback, or modification to this project.

When doing so, please:
- Maintain cross-platform compatibility
- Preserve the existing public contract unless a breaking change is intentional
- Submit focused pull requests that conform to the existing code style
- Maintain your branch synchronised with `master`
- Revise the documentation when functionality changes
- Properly test all modifications, including edge cases and error conditions
- Include tests for additional or modified functionality
- Raise a new [issue](https://github.com/hmlendea/nucigenerators.text.markovchain/issues) for problems or suggestions

See the [contributing guidelines](./CONTRIBUTING.md) for details on how to report issues, suggest enhancements, and submit changes.

## 📜 Code of Conduct

See the [Code of Conduct](./CODE_OF_CONDUCT.md) for the standards of conduct expected in all project spaces.

## 🛡️ Privacy and Data

NuciGenerators.Text.MarkovChain does not collect, process, transmit, or persist personal data, analytics, diagnostics, or telemetry. The library operates entirely in memory with training data provided by the developer.

For the detailed description of how the application handles privacy and personal data, see [PRIVACY.md](./PRIVACY.md).

## 🔒 Security

NuciGenerators.Text.MarkovChain follows secure development practices.

For information on reporting security vulnerabilities, see [SECURITY.md](./SECURITY.md).

## 💝 Project Engagement

Discovered a problem or have a suggestion? [Open an issue](https://github.com/hmlendea/nucigenerators.text.markovchain/issues)!

If you find this project useful, consider [funding it](https://hmlendea.go.ro/funding) or starring ⭐️ it on GitHub!

[![Donate](https://raw.githubusercontent.com/hmlendea/readme-assets/master/donate_generic.png)](https://hmlendea.go.ro/funding)

## 📄 License

This project is being distributed under the `GNU General Public License v3.0 or later`.
See [LICENSE](./LICENSE) for additional information.
