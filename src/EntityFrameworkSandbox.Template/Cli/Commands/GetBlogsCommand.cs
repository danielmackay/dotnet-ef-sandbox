﻿using EntityFrameworkSandbox.Template.Cli.Common;
using EntityFrameworkSandbox.Template.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Spectre.Console;
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
        AnsiConsole.WriteLine("Getting Blogs");

        var blogs = await _db.Blogs.Select(b => new
        {
            b.BlogId,
            b.Url,
        }).ToListAsync();

        foreach (var blog in blogs)
            AnsiConsole.Console.WriteJson(blog);

        return 0;
    }
}
