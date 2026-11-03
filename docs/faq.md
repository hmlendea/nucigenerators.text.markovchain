# Frequently asked questions

## General

### What is this library for?

Generating pronounceable, novel names that statistically resemble a training dataset. Useful for games, procedural content, test data generation.

### What .NET versions are supported?

.NET 10.0 (`net10.0`) only.

### Is it thread-safe?

Instances are immutable after construction, so safe for concurrent reads. `Random` is not thread-safe; if sharing an instance across threads, synchronize `Generate()` calls or use separate instances.

## Usage

### How do I choose order and prior?

Start with `order=3, prior=0.5f`. Increase order for fidelity, decrease for creativity. Increase prior to reduce overfitting.

### Can I use it for non-English names?

Yes. Character-level Markov chains work with any Unicode characters in the training data.

### How do I prevent duplicates?

Use `GenerationAlgorithm()` (via base `NameGenerator.GenerateName()`) which filters duplicates, or check `Data` manually.

### Can I save/load a trained model?

Not directly. Serialize the training `Word`/`Wordlist` data and reconstruct the generator on load.

## Troubleshooting

### Why does Generate() return "###"?

Training data too small for the order, or all transitions lead to "#". Reduce order or add more data.

### Why are names invalid?

`IsNameValid()` from base class may reject names. Check its implementation in `NuciGenerators.Text`.

## Contributing

### How do I run tests?

```bash
dotnet test NuciGenerators.Text.MarkovChain.UnitTests/
```

### What's the license?

GPL-3.0-or-later.