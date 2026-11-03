# Documentation index

## Repository summary

NuciGenerators.Text.MarkovChain is a .NET 10.0 library for Markov-chain-based name generation. It learns character-level transition probabilities from training words and generates novel names that statistically resemble the source data. The library integrates with the NuciGenerators.Text ecosystem via the `NameGenerator` base class.

## Root document map

- [ARCHITECTURE.md](../ARCHITECTURE.md) — (not present, see [docs/architecture.md](./architecture.md))
- [SECURITY.md](../SECURITY.md) — (not present, see [docs/security.md](./security.md))
- [ROADMAP.md](../ROADMAP.md) — (not present)
- [PRIVACY.md](../PRIVACY.md) — (not present)
- [LICENSE](../LICENSE) — GPL-3.0-or-later
- [README.md](../README.md) — Project overview, installation, quick start

## Documentation catalogue

### Getting started
- [Quick start](./quick-start.md) — Installation and basic usage
- [Repository overview](./repository-overview.md) — Purpose, scope, entry points
- [Repository structure](./repository-structure.md) — Source tree, modules, dependencies

### Architecture & design
- [Architecture](./architecture.md) — High-level decomposition, components, invariants
- [Data model](./data-model.md) — Domain entities, internal models, relationships
- [Design decisions](./design-decisions.md) — Key choices and rationale
- [Invariants](./invariants.md) — System-wide and component invariants

### Components
- [MarkovNameGenerator](./components/markov-name-generator.md) — Public API, construction, generation, validation

### Flows
- [Name generation](./flows/name-generation.md) — End-to-end generation, training, fallback, validation flows

### API reference
- [API reference index](./api-reference/INDEX.md) — Endpoint summary, links
- [MarkovNameGenerator](./api-reference/markov-name-generator.md) — Constructors, Generate, GetLetter, GenerationAlgorithm
- [MarkovModel](./api-reference/markov-model.md) — Constructor, Generate, Train, BuildChains, SelectIndex

### Operations
- [Configuration](./configuration.md) — Constructor parameters, tuning guidance
- [Build and deployment](./build-and-deployment.md) — Build commands, CI/CD, versioning
- [Testing](./testing.md) — Test strategy, organisation, coverage, gaps
- [Dependencies](./dependencies.md) — External, internal, transitive dependencies
- [API usage examples](./api-usage-examples.md) — Code examples for common scenarios

### Quality & safety
- [Error handling](./error-handling.md) — Error taxonomy, handling patterns, recovery
- [Logging](./logging.md) — No logging framework used
- [Security](./security.md) — Threat model, mitigations, compliance
- [State and persistence](./state-and-persistence.md) — In-memory state, no persistence, no caches

### Maintenance
- [Change guide](./change-guide.md) — Common modifications, safe patterns, risk areas
- [Troubleshooting](./troubleshooting.md) — Common issues, debugging tips, performance
- [FAQ](./faq.md) — Frequently asked questions
- [Documentation maintenance](./documentation-maintenance.md) — Ownership, update triggers, coverage status
- [Ambiguities and open questions](./ambiguities-and-open-questions.md) — Known gaps, TODOs, unresolved items

## Navigation aids

### Start here (newcomers)
1. [Quick start](./quick-start.md)
2. [Repository overview](./repository-overview.md)
3. [Architecture](./architecture.md)

### Deep dive (component owners)
1. [Components: MarkovNameGenerator](./components/markov-name-generator.md)
2. [API reference: MarkovNameGenerator](./api-reference/markov-name-generator.md)
3. [API reference: MarkovModel](./api-reference/markov-model.md)
4. [Design decisions](./design-decisions.md)
5. [Change guide](./change-guide.md)

### Flows (debuggers)
1. [Name generation flow](./flows/name-generation.md)
2. [Error handling](./error-handling.md)
3. [Troubleshooting](./troubleshooting.md)

## Maintenance metadata

- Last reviewed: 2026-11-03
- Owner: Horațiu Mlendea
- Coverage: Complete (all planned documents created)