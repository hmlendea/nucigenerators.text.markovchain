# Privacy and Personal Data

This document describes how NuciGenerators.Text.MarkovChain at https://github.com/hmlendea/nucigenerators.text.markovchain handles personal data. It covers the library behaviour and verified integrations described below. Where the software is self-hosted, the instance operator may have separate responsibilities described below.

## 📑 Table of Contents

- [What This Document Covers](#what-this-document-covers)
- [Data We Handle](#data-we-handle)
- [Processing and Use](#processing-and-use)
- [Storage, Retention, and Deletion](#storage-retention-and-deletion)
- [External Processing and Integrations](#external-processing-and-integrations)
- [Document Changes](#document-changes)
- [Contact](#contact)

## 🔎 What This Document Covers

This document describes how NuciGenerators.Text.MarkovChain at https://github.com/hmlendea/nucigenerators.text.markovchain handles personal data. It covers the library behaviour and verified integrations described below. Where the software is self-hosted, the instance operator may have separate responsibilities described below.

## 📥 Data We Handle

### Data Provided to the Application

No personal data is requested by the library. The library accepts training data (lists of words) provided by the developer integrating the library. Any personal data in that training data is controlled by the developer, not by this library.

### Data Generated or Collected by the Application

No personal data is generated or collected automatically by the library. The library generates new words based on statistical patterns learned from the training data provided by the developer.

### Data Received from Integrations

No personal data is received from integrations or third parties. The library has no built-in integrations with external services.

## 🧭 Processing and Use

The library processes the data described above for these verified functions:
- Markov model training — Training words provided by the developer
- Name generation — Statistical character transitions derived from training words

## 🗄️ Storage, Retention, and Deletion

The library does not persist any data. All models exist only in memory during the lifetime of the `MarkovNameGenerator` instance. The developer controls the training data lifecycle and any generated output. No logs, caches, backups, or configured storage services are used by the library.

## 🔗 External Processing and Integrations

The library has no built-in external data transfer. No external services, recipients, or integrations process or receive data from this library.

| Service or integration | Purpose | Data involved | Configuration or documentation |
|-----------------------|---------|---------------|--------------------------------|
| None | Not applicable | Not applicable | Not applicable |

## 🔄 Document Changes

Update this document when library data flows, storage, integrations, or deployment responsibilities change. The current version is published at https://github.com/hmlendea/nucigenerators.text.markovchain/blob/master/PRIVACY.md.

## 📬 Contact

For questions about library data handling, contact the project maintainers at https://github.com/hmlendea/nucigenerators.text.markovchain/issues. Include the library version and a description of the data flow in question; do not send passwords, access tokens, or other secrets.