# Flows

## Name generation flow

```
Generate()
    │
    ▼
context = "#".Repeat(Order)
    │
    ▼
GetLetter(context) ──────────────────────────┐
    │                                        │
    ▼                                        │
letter = null                                │
myContext = context.Substring(context.Length - Order, Order)
    │                                        │
    ▼                                        │
For each model in models (highest→lowest)    │
    │                                        │
    ▼                                        │
model.Generate(myContext)                    │
    │                                        │
    ├─► returns letter ──────────────────────┤
    │                                        │
    └─► returns null                         │
         │                                   │
         ▼                                   │
    myContext = myContext[1..] (backoff)     │
         │                                   │
         └───────────────────────────────────┘
    │
    ▼
If letter == "#" → terminate, return word
If letter != null → word += letter
    │
    ▼
context = word (full accumulated word)
    │
    ▼
Loop back to GetLetter(context)
```

## Training flow

```
Constructor(List<Wordlist> data, int order, float prior)
    │
    ▼
Merge all Wordlists into List<Word> Data
    │
    ▼
Extract all characters from all word Values
    │
    ▼
Sort characters, distinct → alphabet
    │
    ▼
Prepend "#" to alphabet → domain
    │
    ▼
For i = 0 to Order-1:
    │
    ▼
Create MarkovModel(wordValues, Order-i, Prior, domain)
    │
    ▼
Add to models list
```

## MarkovModel training flow

```
MarkovModel(data, order, prior, alphabet)
    │
    ▼
Train(data)
    │
    ▼
For each word in data:
    │
    ▼
wrapped = "#".Repeat(order) + word + "#"
    │
    ▼
For i = 0 to wrapped.Length - order:
    │
    ▼
context = wrapped.Substring(i, order)
nextChar = wrapped[i + order]
    │
    ▼
observations[context].Add(nextChar)
    │
    ▼
BuildChains()
    │
    ▼
For each context in observations.Keys:
    │
    ▼
For each prediction in alphabet:
    │
    ▼
matches = CountMatches(observations[context], prediction)
weight = prior + matches
    │
    ▼
chains[context].Add(weight)
```

## Generation with fallback detail

```
GetLetter(context)
    │
    ▼
myContext = last Order chars of context
    │
    ▼
For model in models (index 0 = highest order):
    │
    ▼
letter = model.Generate(myContext)
    │
    ├─► letter != null → return letter
    │
    └─► letter == null
         │
         ▼
    myContext = myContext[1..] (drop first char)
         │
         ▼
    If myContext.Length < model.order → continue to next model
    Else → retry same model with shorter context
    │
    ▼
If all models exhausted → return null
```

## Validation flow (GenerationAlgorithm)

```
GenerationAlgorithm()
    │
    ▼
name = Generate()
    │
    ▼
name = name.Replace("#", "")
    │
    ▼
IsNameValid(name) ───► false → return null
    │
    ▼
Data.Any(w => w.Values.Contains(name)) ───► true → return null
    │
    ▼
return name
```