
namespace LocalRagConsoleDemo.Models
{
    public class Document
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
        // Add embedding field if you're storing vector embeddings
        public float[] Embedding { get; set; }
    }
}