using GameDeals.Data.Contracts.Model;
using System.Data.Entity;

namespace GameDeals.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
            :base("GameDeals")
        {
        }

        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Feed> Feeds { get; set; }
        public virtual DbSet<Post> Posts { get; set; }
    }
}
