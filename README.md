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

### Template Installation

Install the dotnet CLI template via:

```ps1
dotnet new --install EntityFrameworkSandbox.Template 
```

### Project Creation

You can use the template to create a new project via:

```ps1
mkdir my-ef-sandbox
cd my-ef-sandbox
dotnet new ef-sandbox --name EF.Sandbox --output .\
```

Alternatively, you can create the project directly into a new sub-folder via:

```ps1
dotnet new ef-sandbox --name EF.Sandbox
```

## Usage

### Initializing the Database

```ps1
dotnet run init
```

### Running Commands

```ps1
dotnet run get-posts
dotnet run get-blogs
```

## Customization

### Writing Commands & Queries

New commands can be added to `/Cli/Commands/`.  

For example:

```csharp
public class GetBlogsCommand : AsyncCommand
{
    private readonly BloggingContext _db;
    private readonly ILogger<GetBlogsCommand> _logger;

    public GetBlogsCommand(BloggingContext context, ILogger<GetBlogsCommand> logger)
    {
        _db = context;
        _logger = logger;
    }

    public override async Task<int> ExecuteAsync(CommandContext context)
    {
        _logger.LogInformation("Getting Blogs...");

        var blogs = await _db.Blogs.ToListAsync();

        foreach (var blog in blogs)
            _logger.LogInformation(blog.ToString());

        return 0;
    }
}
```

### Overriding Model Configuration

This can be done in the configuration classes:

```csharp
internal class PostConfiguration : IEntityTypeConfiguration<Post>
{
    public void Configure(EntityTypeBuilder<Post> builder)
    {
        // NOTE: Custom model configuration goes here
    }
}
```

### Overriding the Connection String

Leave `appsettings.json` as is and add a secret via the CLI.  This ensures your connection does not get checked into source control

```ps1
dotnet user-secrets set "ConnectionStrings:Default" "{Your Connection String}"
```

### Schema Changes

The project is designed to use migrations for schema upgrades.  However, if you prefer to instead drop and create the DB every time you can set `Application.EnableMigrations` to `false` in `appsettings.json`:

```json
"Application": {
    "EnableMigrations": false
}
```

## Troubleshooting

- Ensure the connection matches if you are using something other than local DB

## Deployment
  
### Updated Nuget Version

A new package will be pushed to Nuget anytime `EntityFrameworkSandbox.Template.nuspec` is changed on `main` branch.  Normally this will happen via a package version change.
