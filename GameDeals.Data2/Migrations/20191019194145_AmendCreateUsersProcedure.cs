﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace GameDeals.Data2.Migrations
{
    public partial class AmendCreateUsersProcedure : Migration
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
