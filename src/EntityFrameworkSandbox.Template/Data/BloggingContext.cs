using EntityFrameworkSandbox.Template.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Spectre.Console;
using System.Reflection;

namespace EntityFrameworkSandbox.Template.Data;

public class BloggingContext : DbContext
{
    private readonly IConfiguration _configuration;
    private readonly ILogger<BloggingContext> _logger;

    public DbSet<Blog> Blogs { get; set; }
    public DbSet<Post> Posts { get; set; }
    public DbSet<Tag> Tags { get; set; }

    public BloggingContext(IConfiguration configuration, ILogger<BloggingContext> logger)
    {
        _configuration = configuration;
        _logger = logger;
    }

    // The following configures EF to create a Sqlite database file in the
    // special "local" folder for your platform.
    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.UseSqlServer(_configuration.GetConnectionString("Default"));
        options.EnableDetailedErrors();
        options.EnableSensitiveDataLogging();
        options.LogTo(msg => AnsiConsole.MarkupLineInterpolated($"[yellow]{msg}[/]"), new[] { RelationalEventId.CommandExecuted });
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(builder);
    }
}
