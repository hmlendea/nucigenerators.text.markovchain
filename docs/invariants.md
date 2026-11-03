# Invariants

## System-wide invariants

1. **Order >= 1** — Markov order must be positive (enforced by caller, not validated)
2. **Prior >= 0** — Smoothing weight must be non-negative (enforced by caller, not validated)
3. **models.Length == Order** — One model per order level
4. **models[i].order == Order - i** — Models have decreasing orders: Order, Order-1, ..., 1
5. **Shared alphabet** — All models use identical alphabet (domain characters + "#")
6. **Alphabet contains "#"** — Boundary marker always present at index 0
7. **Generation terminates** — `Generate()` always eventually produces "#" (assuming correct implementation)
8. **Training data preserved** — `Data` property contains merged input words unchanged
9. **Immutability** — `Order`, `Prior`, `Data`, `models` never change after construction

## MarkovModel invariants

1. **chains.Keys == observations.Keys** — Every trained context has a probability chain
2. **chains[context].Count == alphabet.Count** — Weight vector parallel to alphabet
3. **Sum(chains[context]) > 0** — At least one transition has non-zero weight (prior ensures this)
4. **alphabet[0] == "#"** — Boundary marker at known position

## Generation invariants

1. **Context length == Order** — `GetLetter` always extracts exactly `Order` characters
2. **Backoff reduces context** — Each fallback step removes first character
3. **Lowest order model has order=1** — Final fallback always has single-character context
4. **Boundary markers in output** — `Generate()` returns word with "#" prefix and suffix

## Validation invariants (GenerationAlgorithm)

1. **No "#" in returned name** — All boundary markers stripped
2. **IsNameValid(name) == true** — Base class validation passes
3. **name not in training data** — Novelty check against `Data.Values`
4. **Returns null on any failure** — Invalid, duplicate, or generation failure all yield null