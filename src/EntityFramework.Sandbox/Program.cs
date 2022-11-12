// See https://aka.ms/new-console-template for more information
using EntityFramework.Sandbox;
using EntityFramework.Sandbox.Data;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = Host.CreateDefaultBuilder(args);
builder.ConfigureServices(services =>
{
    services.AddDbContext<BloggingContext>();
    services.AddSingleton<BloggingContextInitialiser>();
    services.AddSingleton<Sandbox>();
});

var app = builder.Build();
app.Start();

using (var scope = app.Services.CreateScope())
{
    var sandbox = app.Services.GetRequiredService<Sandbox>();
    await sandbox.Run();

    Console.WriteLine("Done!");
    Console.ReadKey();
}
