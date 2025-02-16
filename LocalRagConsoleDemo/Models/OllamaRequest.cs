namespace LocalRagConsoleDemo.Models;

public class OllamaRequest
{
    public string Model { get; set; } = "phi4";
    public string Prompt { get; set; } = string.Empty;
    public Dictionary<string, object>? Options { get; set; }
}