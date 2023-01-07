using EntityFrameworkSandbox.Template.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Spectre.Console;
using Spectre.Console.Cli;

namespace EntityFrameworkSandbox.Template.Cli.Commands;

public class GetPostsCommand : AsyncCommand
{
    private readonly BloggingContext _db;
    private readonly ILogger<GetPostsCommand> _logger;

    public GetPostsCommand(BloggingContext context, ILogger<GetPostsCommand> logger)
    {
        _db = context;
        _logger = logger;
    }

    public override async Task<int> ExecuteAsync(CommandContext context)
    {
        _logger.LogInformation("Getting Posts...");

        var posts = await _db.Posts.ToListAsync();
        
        foreach (var post in posts)
            _logger.LogInformation(post.ToString());

        return 0;
    }
}
