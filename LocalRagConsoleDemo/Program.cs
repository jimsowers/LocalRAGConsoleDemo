using Microsoft.Extensions.DependencyInjection;
using LocalRagConsoleDemo.Services;
using System.Text;

var services = new ServiceCollection();
services.AddHttpClient();
services.AddScoped<IOllamaService, OllamaService>();

var serviceProvider = services.BuildServiceProvider();

var ollamaService = serviceProvider.GetRequiredService<IOllamaService>();

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