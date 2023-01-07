using EntityFrameworkSandbox.Template.Cli.Commands;
using EntityFrameworkSandbox.Template.Cli.Common;
using EntityFrameworkSandbox.Template.Data;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Spectre.Console;
using Spectre.Console.Cli;
using System.Reflection;

AnsiConsole.Write(new FigletText("EF Sandbox").Color(Color.Purple));
AnsiConsole.WriteLine($"Entity Framework Sandbox Command-line Tools {Assembly.GetExecutingAssembly().GetName().Version}");
AnsiConsole.WriteLine();

var builder = Host.CreateDefaultBuilder(args);

// Add services to the container
builder.ConfigureServices(services =>
{
    services.AddDbContext<BloggingContext>(contextLifetime: ServiceLifetime.Transient, optionsLifetime: ServiceLifetime.Transient);
    services.AddTransient<BloggingContextInitialiser>();
});

var registrar = new TypeRegistrar(builder);
var app = new CommandApp(registrar);

app.Configure(config =>
{
    config.PropagateExceptions();

    // Register available commands
    config.AddCommand<InitCommand>("init");
    config.AddCommand<GetPostsCommand>("get-posts");
    config.AddCommand<GetBlogsCommand>("get-blogs");
});

try
{
    return app.Run(args);
}
catch (Exception ex)
{
    AnsiConsole.WriteException(ex, ExceptionFormats.ShortenEverything);
    return -99;
}
