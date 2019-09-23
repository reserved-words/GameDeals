namespace GameDeals.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    using SqlResources = GameDeals.Data.Resources;

    public partial class FixAnotherBugInCreateUsersProc : DbMigration
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
