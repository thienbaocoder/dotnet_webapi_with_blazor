//ef-context-ip
using Microsoft.EntityFrameworkCore;

namespace webapi_blazor.Models
{
    public class StoreCybersoftContext : DbContext
    {
        public StoreCybersoftContext() { }
        public StoreCybersoftContext(DbContextOptions<StoreCybersoftContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=localhost,1434;Database=StoreCybersoft;User Id=sa;Password=BaoCybersoft123@;TrustServerCertificate=true;");
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
    }
}