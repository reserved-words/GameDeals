using Microsoft.EntityFrameworkCore.Migrations;

namespace GameDeals.Data.Migrations
{
    public partial class AmendCreateUsersProcedureAgain : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(Resources.CreateProcedure_CreateUsers);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(Resources.DropProcedure_CreateUsers);
        }
    }
}
