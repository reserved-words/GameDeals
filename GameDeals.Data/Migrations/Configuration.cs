using System.Data.Entity.Migrations;

namespace GameDeals.Data.Migrations
{
    public sealed class Configuration : DbMigrationsConfiguration<ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "GameDeals.Data.ApplicationDbContext";
        }
    }
}
