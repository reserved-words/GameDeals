using GameDeals.Data.Contracts.Model;
using Microsoft.EntityFrameworkCore;

namespace GameDeals.Data2
{
    public class ApplicationDbContext : DbContext
    {
        private readonly string _connectionString;
        private readonly string _schemaName;

        public ApplicationDbContext()
            :this("Data Source=(LocalDb)\\MSSQLLocalDB;Initial Catalog=GameDeals;Integrated Security=True;", "dbo")
        {
        }

        public ApplicationDbContext(string connectionString, string schemaName)
        {
            _connectionString = connectionString;
            _schemaName = schemaName;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }

        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Feed> Feeds { get; set; }
        public virtual DbSet<Post> Posts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema(_schemaName);
        }
    }
}
