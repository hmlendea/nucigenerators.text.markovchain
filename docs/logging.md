# Logging

## Logging framework

None. This library does not use any logging framework (Serilog, NLog, log4net, etc.).

## Log levels

Not applicable.

## Structured fields

Not applicable.

## Correlation

Not applicable — no distributed tracing, no correlation IDs.

## Sinks/destinations

Not applicable.

## Redaction/masking

Not applicable.

## Retention

Not applicable.

## Sampling

Not applicable.

## Performance considerations

No logging overhead.

## Error logging

Exceptions are not logged. Errors manifest as:
- `null` returns from `Generate()`, `GetLetter()`, `GenerationAlgorithm()`
- Potential exceptions from BCL (ArgumentNullException, IndexOutOfRangeException)

## Audit logging

No audit logging. No security-relevant events tracked.

## Configuration

No logging configuration.