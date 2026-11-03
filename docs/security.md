# Security

## Security model

This library is an in-process .NET library with no network access, no file I/O, no external service calls, and no user input handling. It operates entirely on data provided by the caller.

## Threats

| Threat | Applicability | Mitigation |
|--------|---------------|------------|
| Injection attacks | None — no SQL, shell, or template execution | N/A |
| Data exfiltration | None — no output channels except return values | N/A |
| Denial of service | Possible via malicious training data causing excessive memory | Caller should validate/limit input size |
| Randomness predictability | `System.Random` not cryptographically secure | Not a security boundary; use `RandomNumberGenerator` if needed |

## Mitigations

- No external dependencies with known vulnerabilities (NuciExtensions, NuciGenerators.Text)
- No unsafe code, no P/Invoke
- No serialization/deserialization of untrusted data
- Immutable configuration prevents runtime tampering

## Sensitive data

- Training data provided by caller may contain sensitive information
- Library does not log, persist, or transmit training data
- Caller responsible for data handling compliance

## Compliance

- GPL-3.0-or-later license
- No telemetry, no analytics, no network calls