using Bogus;
using EntityFrameworkSandbox.Template.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace EntityFrameworkSandbox.Template.Data;

public class BloggingContextInitialiser
{
    private readonly ILogger<BloggingContextInitialiser> _logger;
    private readonly BloggingContext _db;
    private readonly IOptions<DataConfig> _config;

    public BloggingContextInitialiser(ILogger<BloggingContextInitialiser> logger, BloggingContext context, IOptions<DataConfig> config)
    {
        _logger = logger;
        _db = context;
        _config = config;
    }

    public async Task Run()
    {
        _logger.LogInformation("Initializing DB");
        await InitialiseAsync();

        _logger.LogInformation("Seeding DB");
        await SeedAsync();
    }

    private async Task InitialiseAsync()
    {
        try
        {
            if (_db.Database.IsSqlServer())
            {
                var isMigrationsEnabled = _config.Value.EnableMigrations;

                if (isMigrationsEnabled)
                {
                    await _db.Database.MigrateAsync();
                }
                else
                {
                    await _db.Database.EnsureDeletedAsync();
                    await _db.Database.EnsureCreatedAsync();
                }

            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while initialising the database.");
            throw;
        }
    }

    private async Task SeedAsync()
    {
        try
        {
            await TrySeedAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while seeding the database.");
            throw;
        }
    }

    private async Task TrySeedAsync()
    {
        // Default data
        // Seed, if necessary
        if (!_db.Blogs.Any())
        {
            var tagNames = new string[] { "Architecture", "Powershell", "Bicep", "Blazor", "Web", "Azure", "Console", ".NET", "JavaScript" };
            var tags = tagNames.Select(x => new Tag { Name = x.ToLower() }).ToList();

            var postFaker = new Faker<Post>()
                .RuleFor(p => p.Title, f => f.Lorem.Sentence())
                .RuleFor(p => p.Content, f => f.Lorem.Paragraphs(3))
                .RuleFor(p => p.Tags, f => f.Random.ListItems(tags, 2));

            var blogFaker = new Faker<Blog>()
                .RuleFor(b => b.Url, f => f.Internet.Url())
                .RuleFor(b => b.Posts, f => postFaker.Generate(_config.Value.PostsPerBlog));

            _db.Tags.AddRange(tags);
            _db.Blogs.AddRange(blogFaker.Generate(_config.Value.Blogs));
            await _db.SaveChangesAsync();
        }
    }
}