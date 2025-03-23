

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Document = LocalRagConsoleDemo.Models.Document;

namespace LocalRagConsoleDemo.Data.Contexts
{
    public class RagDbContext : DbContext
    {
        public RagDbContext(DbContextOptions<RagDbContext> options) : base(options) { }
        
        public DbSet<Document> Documents { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            // Configure the Document entity
            modelBuilder.Entity<Document>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Title).IsRequired();
                entity.Property(e => e.Content).IsRequired();
                
                // If you're using vector embeddings
                // entity.Property(e => e.Embedding).HasColumnType("real[]");
            });
        }
    }

    public class RagDbContextFactory : IDesignTimeDbContextFactory<RagDbContext>
    {
        public RagDbContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<RagDbContext>();
            optionsBuilder.UseNpgsql(configuration.GetConnectionString("DefaultConnection"));

            return new RagDbContext(optionsBuilder.Options);
        }
    }
}