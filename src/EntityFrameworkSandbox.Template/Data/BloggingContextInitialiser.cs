using EntityFrameworkSandbox.Template.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EntityFrameworkSandbox.Template.Data;

public class BloggingContextInitialiser
{
    private readonly ILogger<BloggingContextInitialiser> _logger;
    private readonly BloggingContext _db;

    public BloggingContextInitialiser(ILogger<BloggingContextInitialiser> logger, BloggingContext context)
    {
        _logger = logger;
        _db = context;
    }

    public async Task InitialiseAsync()
    {
        try
        {
            if (_db.Database.IsSqlServer())
            {
                await _db.Database.EnsureDeletedAsync();
                await _db.Database.EnsureCreatedAsync();
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while initialising the database.");
            throw;
        }
    }

    public async Task SeedAsync()
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

    public async Task TrySeedAsync()
    {
        // Default data
        // Seed, if necessary
        if (!_db.Blogs.Any())
        {
            var architectureTag = new Tag { Name = "Architecture" };
            var powershellTag = new Tag { Name = "Powershell" };
            var bicepTag = new Tag { Name = "Bicep" };
            var blazorTag = new Tag { Name = "Blazor" };
            var webTag = new Tag { Name = "Web" };
            var consoleTag = new Tag { Name = "Console" };
            var dotnetTag = new Tag { Name = ".NET" };

            _db.Tags.AddRange(
                architectureTag,
                powershellTag,
                bicepTag,
                blazorTag,
                webTag,
                consoleTag,
                dotnetTag);

            _db.Blogs.AddRange(
            new Blog
            {
                Url = "https://www.dandoescode.com/",
                Posts = new List<Post>
                {
                    new Post
                    {
                        Title = "Software Diagrams - Plant UML vs Mermaid",
                        Content = "Before jumping into any complex software development, it’s often a good idea to spend some time designing the system or feature you will be working on. A design is easy and quick to change. A software implementation on the other hand, is not",
                        Tags = new List<Tag> {architectureTag}
                    },
                    new Post
                    {
                        Title = "Introduction to PowerShell Scripting",
                        Content = "Reference article covering: installation and setup, variables, parameters, inputs/outputs, arrays, hashtables, flow control, loops, functions, debugging, error handling, filtering, sorting, projecting, formatting, and the help system.",
                        Tags = new List<Tag> {powershellTag}
                    },
                    new Post
                    {
                        Title = "Bicep - Part 2: Advanced Concepts and Features",
                        Content = "Deep dive into Bicep templates including expressions, template logic, and ARM template decompilation, modules and best practices.\r\n\r\n",
                        Tags = new List<Tag> { bicepTag }
                    }
                }
            },
            new Blog
            {
                Url = "https://jasontaylor.dev/",
                Posts = new List<Post>
                {
                    new Post
                    {
                        Title = "Clean Architecture with .NET Core: Getting Started",
                        Content = "This post provides an overview of Clean Architecture and introduces the new Clean Architecture Solution Template, a .NET Core Project template for building applications based on Angular, ASP.NET Core 3.1, and Clean Architecture.",
                        Tags = new List<Tag> {architectureTag, webTag, dotnetTag }
                    },
                    new Post
                    {
                        Title = "Console App Project Template for .NET 7",
                        Content = "In a previous blog post Developing Console Apps with .NET, I demonstrated how to develop command-line programs from scratch including support for built-in help, arguments, configuration, logging, dependency injection, and …",
                        Tags = new List<Tag> {consoleTag, dotnetTag }
                    },
                    new Post
                    {
                        Title = "Clean Architecture Solution Template for Blazor WebAssembly",
                        Content = "This week I released a new solution template to support creating Blazor WebAssembly applications hosted on ASP.NET Core and following the principles of Clean Architecture. With this new template, my …",
                        Tags = new List<Tag> {architectureTag, blazorTag, dotnetTag }
                    }
                }
            });

            await _db.SaveChangesAsync();
        }
    }
}
