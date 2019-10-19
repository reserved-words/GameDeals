using Microsoft.EntityFrameworkCore.Migrations;
using SqlResources = GameDeals.Data2.Resources;

namespace GameDeals.Data2.Migrations
{
    public partial class AddCreateUsersStoredProcedure : Migration
    {
        protected override void Up(MigrationBuilder builder)
        {
            builder.Sql(SqlResources.CreateProcedure_CreateUsers);
        }

        protected override void Down(MigrationBuilder builder)
        {
            builder.Sql(SqlResources.DropProcedure_CreateUsers);
        }
    }
}
