using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DotNet.Ef7.Sandbox.Data;

public class BloggingContext : DbContext
{
    private readonly IConfiguration _configuration;

    public DbSet<Blog> Blogs { get; set; }
    public DbSet<Post> Posts { get; set; }

    public string DbPath { get; }

    public BloggingContext(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    // The following configures EF to create a Sqlite database file in the
    // special "local" folder for your platform.
    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlServer(_configuration.GetConnectionString("Default"));
}
