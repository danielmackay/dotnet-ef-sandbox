using EntityFrameworkSandbox.Template.Cli.Common;
using EntityFrameworkSandbox.Template.Data;
using EntityFrameworkSandbox.Template.Data.Entities;
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
        AnsiConsole.WriteLine("Getting Posts");

        var posts = await _db.Posts.ToListAsync();
        
        foreach (var post in posts)
            AnsiConsole.Console.WriteJson(post);

        return 0;
    }
}
