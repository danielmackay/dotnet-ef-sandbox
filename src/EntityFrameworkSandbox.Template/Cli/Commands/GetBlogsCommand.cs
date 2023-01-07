using EntityFrameworkSandbox.Template.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Spectre.Console.Cli;

namespace EntityFrameworkSandbox.Template.Cli.Commands;

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
