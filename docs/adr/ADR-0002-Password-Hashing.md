# ADR-0002

## Title:
Password Hashing Strategy

## Decision:
Use ASP.NET Core PasswordHasher<TUser>.

## Context:
Invoice Builder requires secure password storage.
There are no legacy password hashes and no requirement
for a specific external hashing algorithm.

## Alternatives:
- BCrypt
- Argon2id
- PBKDF2
- ASP.NET Core PasswordHasher
