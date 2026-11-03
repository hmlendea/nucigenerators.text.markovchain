# Troubleshooting

## Common issues

### Generated names are just "#" markers

**Cause**: Training data empty, or order too high for data size, or prior too high

**Fix**:
- Ensure training data has sufficient words
- Reduce `order` (try 1-2)
- Reduce `prior` (try 0.1-0.5)

### Generated names duplicate training data

**Cause**: `GenerationAlgorithm()` returns `null` for duplicates, but caller may use `Generate()` directly

**Fix**: Use `GenerationAlgorithm()` (via base class `GenerateName()`) instead of `Generate()`, or check for duplicates manually

### Generation returns null

**Cause**: `GenerationAlgorithm()` validation failed (invalid name or duplicate)

**Fix**:
- Check `IsNameValid()` implementation in base class
- Increase training data diversity
- Adjust `order`/`prior` for more variation

### OutOfMemoryException with large training data

**Cause**: `observations` and `chains` dictionaries grow with unique contexts × alphabet size

**Fix**:
- Limit training data size
- Reduce `order`
- Consider streaming/chunked training (not implemented)

### Non-deterministic test failures

**Cause**: `Random` usage with `prior > 0`

**Fix**: Use `prior = 0` for deterministic tests, or inject fixed-seed `Random`

### IndexOutOfRangeException in GetLetter

**Cause**: `context.Length < Order` when calling `GetLetter`

**Fix**: Ensure caller passes context with at least `Order` characters (prefix with "#".Repeat(Order))

## Debugging tips

1. **Inspect alphabet**: Check `models[0].alphabet` contains expected characters + "#"
2. **Check observations**: `models[0].observations` shows learned transitions
3. **Trace GetLetter**: Add logging to see context, model tried, result
4. **Verify training**: With `prior=0`, single word should reproduce exactly

## Performance

- Training: O(N × L × Order) where N=words, L=avg length
- Generation: O(Length × Order × Models) per character
- Memory: O(UniqueContexts × AlphabetSize)