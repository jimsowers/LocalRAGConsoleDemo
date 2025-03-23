using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using LocalRagConsoleDemo.Services;
using LocalRagConsoleDemo.Data;
using LocalRagConsoleDemo.Data.Repositories;
using System.Text;
using LocalRagConsoleDemo.Data.Contexts;

// Create configuration
IConfiguration configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .Build();

// Set up services
var services = new ServiceCollection();

// Configure DbContext
services.AddDbContext<RagDbContext>(options =>
    options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

// Add services
services.AddHttpClient();
services.AddScoped<IOllamaService, OllamaService>();
services.AddScoped<IDocumentRepository, DocumentRepository>();

var serviceProvider = services.BuildServiceProvider();

// Get services
var ollamaService = serviceProvider.GetRequiredService<IOllamaService>();
var documentRepository = serviceProvider.GetRequiredService<IDocumentRepository>();

// Test database connection
await TestDatabaseConnection();

// Main prompt loop
Console.WriteLine("Enter your prompt (or 'exit' to quit):");
while (true)
{
    var prompt = Console.ReadLine();
    if (string.IsNullOrEmpty(prompt) || prompt.ToLower() == "exit")
    {
        break;
    }
    try
    {
        Console.WriteLine("\nPhi-4 Response:");
        var responseBuilder = new StringBuilder();
        await foreach (var token in ollamaService.GetCompletionStreamAsync(prompt))
        {
            if (!string.IsNullOrEmpty(token))
            {
               // Console.Write(token);
                responseBuilder.Append(token);
                await Console.Out.FlushAsync();
            }
        }
        Console.WriteLine("\n");
        Console.WriteLine(responseBuilder.ToString());
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error: {ex.Message}");
        Console.WriteLine($"Stack trace: {ex.StackTrace}");
    }
    Console.WriteLine("\nEnter your prompt (or 'exit' to quit):");
}

// Test database connection method
async Task TestDatabaseConnection()
{
    try
    {
        var newDoc = new LocalRagConsoleDemo.Models.Document
        {
            Title = "Test Document",
            Content = "This is a test document for RAG.",
            CreatedAt = DateTime.UtcNow
        };
        
        await documentRepository.AddDocumentAsync(newDoc);
        Console.WriteLine("Database connection successful - added test document.");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Database connection error: {ex.Message}");
    }
}