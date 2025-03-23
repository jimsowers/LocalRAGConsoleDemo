using LocalRagConsoleDemo.Models;

namespace LocalRagConsoleDemo.Data.Repositories
{
    public interface IDocumentRepository
    {
        Task<IEnumerable<Document>> GetAllDocumentsAsync();
        Task<Document> GetDocumentByIdAsync(int id);
        Task<IEnumerable<Document>> SearchDocumentsAsync(string query);
        Task AddDocumentAsync(Document document);
        Task UpdateDocumentAsync(Document document);
        Task DeleteDocumentAsync(int id);
    }
}