using GameDeals.Data.Contracts.Model;
using Microsoft.EntityFrameworkCore;

namespace GameDeals.Data2
{
    public class ApplicationDbContext : DbContext
    {
        public static string SchemaName = "GameDeals";

        private const string DefaultConnectionString = "Data Source=(LocalDb)\\MSSQLLocalDB;Initial Catalog=GameDeals;Integrated Security=True;";
        
        private readonly string _connectionString;

        public ApplicationDbContext()
            :this(DefaultConnectionString)
        {
        }

        public ApplicationDbContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString, x => x.MigrationsHistoryTable("__MigrationsHistory", SchemaName));
        }

        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Feed> Feeds { get; set; }
        public virtual DbSet<Post> Posts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema(SchemaName);
        }
    }
}
