using EntityFrameworkSandbox.Template.Data;
using Microsoft.Extensions.Logging;
using Spectre.Console;
using Spectre.Console.Cli;

namespace EntityFrameworkSandbox.Template.Cli.Commands;

public class InitCommand : AsyncCommand
{
    private readonly BloggingContextInitialiser _initialiser;
    private readonly ILogger<InitCommand> _logger;

    public InitCommand(BloggingContextInitialiser initialiser, ILogger<InitCommand> logger)
    {
        _initialiser = initialiser;
        _logger = logger;
    }

    public override async Task<int> ExecuteAsync(CommandContext context)
    {
        AnsiConsole.WriteLine("Initializing DB");
        await _initialiser.Run();

        return 0;
    }
}
