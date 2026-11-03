# Design decisions

## Character-level vs word-level Markov chains

**Decision**: Character-level n-grams

**Rationale**:
- Generates pronounceable novel words, not just recombinations of training words
- Works with any language/script without tokenization
- Smaller alphabet than vocabulary size

**Trade-off**: Requires more training data for coherent results; higher orders needed for structure.

## Additive smoothing (prior)

**Decision**: Fixed prior weight added to all transition counts

**Rationale**:
- Simple, well-understood technique (Laplace/Lidstone smoothing)
- Prevents zero probabilities for unseen transitions
- Single parameter controls overfitting vs fidelity

**Trade-off**: Uniform prior may not suit all contexts; no backoff to lower-order distributions within a model.

## Fallback chain (multiple models)

**Decision**: Create `Order` models with decreasing order; try highest first, back off on missing context

**Rationale**:
- Higher orders capture more structure but have sparser coverage
- Lower orders provide robust fallback
- No interpolation between orders (simpler than Kneser-Ney)

**Trade-off**: Abrupt transitions between orders; no blending of predictions.

## Boundary marker (#)

**Decision**: Prepend `Order` "#" markers, append one "#" marker

**Rationale**:
- Explicitly models word start and end
- Allows generation to terminate naturally
- Same marker for start and end (simplifies alphabet)

**Trade-off**: Start and end contexts conflated; cannot distinguish "word beginning" from "word ending" patterns.

## Random sampling vs argmax

**Decision**: Weighted random sampling from probability distribution

**Rationale**:
- Generates diverse outputs
- Reflects uncertainty in model
- Standard approach for generative models

**Trade-off**: Non-deterministic; harder to test. Mitigated by `prior=0` for deterministic tests.

## Immutable configuration

**Decision**: All configuration set in constructor, no setters

**Rationale**:
- Thread-safe by default
- Predictable behavior
- Matches functional style

**Trade-off**: Must create new instance to change parameters.

## No serialization

**Decision**: Models not serializable

**Rationale**:
- Contains `Random` instance (not serializable)
- Training is fast enough to repeat
- Avoids versioning complexity

**Trade-off**: Cannot persist trained models; must retrain on startup.

## Inheritance from NameGenerator

**Decision**: `MarkovNameGenerator : NameGenerator` from NuciGenerators.Text

**Rationale**:
- Reuses validation logic (`IsNameValid`)
- Consistent with NuciGenerators.Text ecosystem
- Provides `GenerationAlgorithm()` override point

**Trade-off**: Couples to external library; base class behavior affects output.