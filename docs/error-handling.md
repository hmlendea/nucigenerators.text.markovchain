# Error handling

## Error taxonomy

| Category | Examples | Handling |
|----------|----------|----------|
| **Invalid configuration** | `order < 1`, `prior < 0`, null data | Not validated — undefined behavior |
| **Empty training data** | `data.Count == 0` | Alphabet = ["#"], models created but generate only "#" |
| **Missing context** | `GetLetter` context not in any model | Returns `null`, triggers backoff |
| **Generation failure** | All models return `null` | `Generate()` returns string of "#" markers |
| **Validation failure** | `IsNameValid` false, or duplicate of training data | `GenerationAlgorithm()` returns `null` |

## Handling patterns

### Constructor

No validation of `order` or `prior` parameters. Caller must ensure:
- `order >= 1`
- `prior >= 0`
- `data` not null, not empty

Violations produce undefined behavior (e.g., `order=0` causes `Repeat(0)` = empty string, breaking context extraction).

### Generation

```csharp
public string Generate()
{
    string word = "#".Repeat(Order);
    string letter = GetLetter(word);

    while(letter.NotEquals("#"))
    {
        if (letter is not null)
        {
            word += letter;
        }
        letter = GetLetter(word);
    }
    return word;
}
```

- Never throws
- Always terminates (eventually produces "#" or loops infinitely if bug)
- Returns word with "#" markers included

### GetLetter fallback

```csharp
public string GetLetter(string context)
{
    string myContext = context.Substring(context.Length - Order, Order);

    foreach (MarkovModel model in models)
    {
        string letter = model.Generate(myContext);
        if (letter is null)
        {
            myContext = myContext[1..];
        }
        else
        {
            break;
        }
    }
    return letter;
}
```

- Tries each model in order
- Shortens context on failure (backoff)
- Returns `null` if all models fail

### MarkovModel.Generate

```csharp
public string Generate(string context)
{
    List<float> chain = chains.TryGetValue(context);
    if (chain is null)
    {
        return null;
    }
    return alphabet[SelectIndex(chain)];
}
```

- Returns `null` for unknown contexts (no exception)
- Assumes `context.Length == order` (caller responsibility)

## Recovery

No automatic recovery. Caller must handle `null` returns:
- `Generate()` → check for all-"#" result
- `GetLetter()` → check for `null`
- `GenerationAlgorithm()` → check for `null` (invalid or duplicate)

## Logging

No built-in logging. Errors/failures not logged.

## Exceptions

No custom exceptions. No try/catch in library code. Potential exceptions:
- `ArgumentNullException` from BCL methods if null passed
- `IndexOutOfRangeException` if `context.Length < Order` in `GetLetter`
- `NullReferenceException` if `data` is null in constructor