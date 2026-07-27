# ADR 0001: Use Modular Monolith Architecture

## Status

Accepted

## Context

Invoice Builder needs a scalable architecture while remaining simple enough for a solo developer.

Microservices would introduce unnecessary complexity:
- Multiple deployments
- Network communication
- Infrastructure overhead

The application requires clear boundaries between business areas while keeping deployment simple.

## Decision

We will use a Modular Monolith architecture.

The application will be organized into modules:

- Invoicing Module
- Customers Module
- Senders Module

Each module owns:

- Business logic
- Entities
- Data access
- Endpoints

The application will be deployed as a single application.

## Alternatives Considered

### Microservices

Rejected because:

- Too complex for current scope
- Higher operational overhead
- Not required for v1.0

### Traditional Layered Architecture

Rejected because:

- Business boundaries become unclear
- Harder to evolve as features grow

## Consequences

Positive:

- Simple deployment
- Clear module boundaries
- Easier future migration to microservices if needed
- Good maintainability

Negative:

- Requires discipline to avoid modules becoming coupled