using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    public class DataContext : DbContext
    {
        public DbSet<GeneratedData> GeneratedData { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=localhost\\sqlexpress;Database=GeneratedDataDb;Trusted_Connection=True;TrustServerCertificate=True;");
        }
    }
}
