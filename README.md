# Entity Framework Sandbox

The Entity Framework Sandbox is a CLI project template that allows you to quickly spin up a functioning project running on the latest version of EF with a real database and real data.

This can be useful in the following scenario:

- Exploring new features of EF such as
- Safely exploring changes to a real application
- Replicating EF issues in an isolated environment

> This is not intended to be a starting point for a production application.

## Prerequisites

- VS2022
- .NET 7
- LocalDB

## Setup

- Ensure the connection matches if you are using something other than local DB
- Update the `Sandbox` to run your own EF queries and commands

```csharp
private async Task RunQueries()
{
    // NOTE: Further DB queries go here
}
```

```csharp
private Task RunCommands()
{
    // NOTE: Further DB commands go here
}
```

## Run

- Press F5
  - Console up will start
  - By default, DB will be dropped & re-created
  - Data will be seeded
  - SQL queries will run
  - All SQL and results output to console