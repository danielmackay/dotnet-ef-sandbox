using EntityFrameworkSandbox.Template;
using EntityFrameworkSandbox.Template.Cli;
using EntityFrameworkSandbox.Template.Data;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Spectre.Console;
using Spectre.Console.Cli;

AnsiConsole.Write(new FigletText("EF Sandbox").Color(Color.Purple));
// TODO DM: Dynamically pull version
AnsiConsole.WriteLine("Entity Framework Sandbox Command-line Tools 0.0.1");
AnsiConsole.WriteLine();

var builder = Host.CreateDefaultBuilder(args);

// Add services to the container
builder.ConfigureServices(services =>
{
    services.AddDbContext<BloggingContext>();
    services.AddTransient<BloggingContextInitialiser>();
    services.AddTransient<Sandbox>();
});

var host = builder.Build();
using (var scope = host.Services.CreateScope())
{
    var initialiser = host.Services.GetRequiredService<BloggingContextInitialiser>();

    AnsiConsole.WriteLine("Initializing DB");
    await initialiser.InitialiseAsync();

    AnsiConsole.WriteLine("Seeding DB");
    await initialiser.SeedAsync();

    AnsiConsole.WriteLine("Done!");
}

var registrar = new TypeRegistrar(builder);

var app = new CommandApp(registrar);

app.Configure(config =>
{
    config.PropagateExceptions();

    //// Register available commands
    //config.AddCommand<WeatherForecastCommand>("forecasts")
    //    .WithDescription("Display local weather forecasts.")
    //    .WithExample(new[] { "forecasts", "5" });
});

// TODO: Migrate to command


try
{
    return app.Run(args);
}
catch (Exception ex)
{
    AnsiConsole.WriteException(ex, ExceptionFormats.ShortenEverything);
    return -99;
}
