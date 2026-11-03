# Quick start

## Installation

```bash
dotnet add package NuciGenerators.Text.MarkovChain
```

## Basic usage

```csharp
using NuciGenerators.Text.MarkovChain;
using NuciGenerators.Text.Models;

// Prepare training data
var trainingWords = new List<Word>
{
    new Word { Id = "example1", Values = ["Alice", "Bob", "Charlie"] },
    new Word { Id = "example2", Values = ["David", "Eve", "Frank"] }
};

// Create generator
// order: context length (2-3 typical)
// prior: smoothing factor (0.2-1.0 typical)
var generator = new MarkovNameGenerator(trainingWords, order: 3, prior: 0.5f);

// Generate names
for (int i = 0; i < 10; i++)
{
    string name = generator.Generate();
    // name includes "#" boundary markers, e.g., "###Alic#"
    // Remove markers for display:
    Console.WriteLine(name.Replace("#", ""));
}
```

## Using Wordlists (grouped data)

```csharp
var wordlists = new List<Wordlist>
{
    new Wordlist
    {
        new Word { Id = "male", Values = ["John", "James", "Robert"] },
        new Word { Id = "female", Values = ["Mary", "Patricia", "Jennifer"] }
    },
    new Wordlist
    {
        new Word { Id = "fantasy", Values = ["Aragorn", "Gandalf", "Frodo"] }
    }
};

var generator = new MarkovNameGenerator(wordlists, order: 2, prior: 0.3f);
```

## Parameter selection

| Goal | Order | Prior |
|------|-------|-------|
| Close to training data | 3-4 | 0.1-0.5 |
| Balanced | 2-3 | 0.5-1.0 |
| Creative/novel | 1-2 | 1.0-2.0 |

## Requirements

- .NET 10.0 or later
- NuciGenerators.Text (transitive dependency)