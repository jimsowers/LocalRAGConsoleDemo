using LocalRagConsoleDemo.Data.Contexts;
using LocalRagConsoleDemo.Models;
using Microsoft.EntityFrameworkCore;

namespace LocalRagConsoleDemo.Data.Repositories
{
    public class DocumentRepository : IDocumentRepository
    {
        private readonly RagDbContext _context;
        
        public DocumentRepository(RagDbContext context)
        {
            _context = context;
        }
        
        public async Task<IEnumerable<Document>> GetAllDocumentsAsync()
        {
            return await _context.Documents.ToListAsync();
        }
        
        public async Task<Document> GetDocumentByIdAsync(int id)
        {
            return await _context.Documents.FindAsync(id);
        }
        
        public async Task<IEnumerable<Document>> SearchDocumentsAsync(string query)
        {
            return await _context.Documents
                .Where(d => d.Title.Contains(query) || d.Content.Contains(query))
                .ToListAsync();
        }
        
        public async Task AddDocumentAsync(Document document)
        {
            await _context.Documents.AddAsync(document);
            await _context.SaveChangesAsync();
        }
        
        public async Task UpdateDocumentAsync(Document document)
        {
            _context.Documents.Update(document);
            await _context.SaveChangesAsync();
        }
        
        public async Task DeleteDocumentAsync(int id)
        {
            var document = await _context.Documents.FindAsync(id);
            if (document != null)
            {
                _context.Documents.Remove(document);
                await _context.SaveChangesAsync();
            }
        }
    }
}