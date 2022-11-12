// See https://aka.ms/new-console-template for more information
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using DotNet.Ef7.Sandbox.Data;

var builder = Host.CreateDefaultBuilder(args);
builder.ConfigureServices(services =>
{
    services.AddDbContext<BloggingContext>();
    services.AddTransient<BloggingContextInitialiser>();
});

var app = builder.Build();

app.Start();

using (var scope = app.Services.CreateScope())
{
    var db = app.Services.GetRequiredService<BloggingContextInitialiser>();
    await db.InitialiseAsync();
    await db.SeedAsync();

    Console.WriteLine("Created DB");

    Console.WriteLine("Done!");
    Console.ReadKey();
}

public class Sandbox
{

}
