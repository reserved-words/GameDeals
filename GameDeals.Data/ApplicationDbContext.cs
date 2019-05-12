using GameDeals.Data.Contracts.Model;
using System.Data.Entity;

namespace GameDeals.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext() : this("GameDeals") { } // default connection string name

        public ApplicationDbContext(string connectionString)
            :base(connectionString)
        {
        }

        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Feed> Feeds { get; set; }
        public virtual DbSet<Post> Posts { get; set; }
    }
}
