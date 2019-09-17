namespace GameDeals.Data.Migrations
{
    using System.Data.Entity.Migrations;
    using SqlResources = GameDeals.Data.Resources;

    public partial class AmendedCreateUsersProcedure : DbMigration
    {
        public override void Up()
        {
            Sql(SqlResources.CreateProcedure_CreateUsers);
        }

        public override void Down()
        {
            Sql(SqlResources.DropProcedure_CreateUsers);
        }
    }
}
