namespace LocalRagConsoleDemo.Models;

public class OllamaResponse
{
    public string Model { get; set; } = string.Empty;
    public string Response { get; set; } = string.Empty;
    public string CreatedAt { get; set; } = string.Empty;
    public bool Done { get; set; }
    public string? DoneReason { get; set; }
    public long? TotalDuration { get; set; }
    public int? PromptEvalCount { get; set; }
    public int? EvalCount { get; set; }
}