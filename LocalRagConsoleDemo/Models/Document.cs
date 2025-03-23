
namespace LocalRagConsoleDemo.Models
{
    // Models/Document.cs
    public class Document
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
        // Make the property nullable
        public float[]? Embedding { get; set; }
    }
}