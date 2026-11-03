# API usage examples

## Basic generation

```csharp
using NuciGenerators.Text.MarkovChain;
using NuciGenerators.Text.Models;

var words = new List<Word>
{
    new Word { Id = "n1", Values = ["Alice", "Bob", "Charlie"] }
};

var generator = new MarkovNameGenerator(words, order: 3, prior: 0.5f);

// Generate 5 names
for (int i = 0; i < 5; i++)
{
    string raw = generator.Generate();        // Includes "#" markers
    string clean = raw.Replace("#", "");      // Remove markers
    Console.WriteLine(clean);
}
```

**Output example:**
```
Alchar
Bobie
Chalib
```

## Using Wordlists for categorized data

```csharp
var wordlists = new List<Wordlist>
{
    new Wordlist
    {
        new Word { Id = "elf", Values = ["Legolas", "Elrond", "Galadriel"] },
        new Word { Id = "dwarf", Values = ["Gimli", "Thorin", "Balin"] }
    },
    new Wordlist
    {
        new Word { Id = "human", Values = ["Aragorn", "Boromir", "Faramir"] }
    }
};

var generator = new MarkovNameGenerator(wordlists, order: 2, prior: 0.3f);
```

## Using GenerationAlgorithm for validated unique names

```csharp
// NameGenerator base class provides GenerateName() which calls GenerationAlgorithm()
var generator = new MarkovNameGenerator(words, order: 3, prior: 0.5f);

string name = generator.GenerateName(); // Returns validated name or null

if (name != null)
{
    Console.WriteLine($"Generated: {name}");
}
else
{
    Console.WriteLine("Generation failed (invalid or duplicate)");
}
```

## Parameter tuning examples

### Conservative (close to training data)
```csharp
var conservative = new MarkovNameGenerator(words, order: 4, prior: 0.1f);
```

### Balanced
```csharp
var balanced = new MarkovNameGenerator(words, order: 3, prior: 0.5f);
```

### Creative (more novel)
```csharp
var creative = new MarkovNameGenerator(words, order: 2, prior: 1.0f);
```

## Inspecting model internals (debugging)

```csharp
var generator = new MarkovNameGenerator(words, order: 2, prior: 0.5f);

// Check alphabet
Console.WriteLine("Alphabet: " + string.Join(", ", generator.models[0].alphabet));

// Check observations for a context
var model = generator.models[0];
if (model.observations.TryGetValue("##", out var nextChars))
{
    Console.WriteLine("After '##': " + string.Join(", ", nextChars));
}
```