using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GameDeals.Data2.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "GameDeals");

            migrationBuilder.CreateTable(
                name: "Categories",
                schema: "GameDeals",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Value = table.Column<string>(maxLength: 30, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Feeds",
                schema: "GameDeals",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryId = table.Column<int>(nullable: false),
                    Title = table.Column<string>(maxLength: 255, nullable: true),
                    Url = table.Column<string>(maxLength: 255, nullable: true),
                    FeedUrl = table.Column<string>(maxLength: 255, nullable: true),
                    LogoFileName = table.Column<string>(maxLength: 60, nullable: true),
                    DaysToExpire = table.Column<int>(nullable: false),
                    ConsecutiveErroredFetches = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Feeds", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Feeds_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalSchema: "GameDeals",
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Posts",
                schema: "GameDeals",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FeedId = table.Column<int>(nullable: false),
                    Title = table.Column<string>(maxLength: 1024, nullable: true),
                    Url = table.Column<string>(maxLength: 1024, nullable: true),
                    PublishedAt = table.Column<DateTime>(nullable: false),
                    FetchedAt = table.Column<DateTime>(nullable: false),
                    Summary = table.Column<string>(nullable: true),
                    Saved = table.Column<bool>(nullable: false),
                    Deleted = table.Column<bool>(nullable: false),
                    Seen = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Posts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Posts_Feeds_FeedId",
                        column: x => x.FeedId,
                        principalSchema: "GameDeals",
                        principalTable: "Feeds",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Feeds_CategoryId",
                schema: "GameDeals",
                table: "Feeds",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Posts_FeedId",
                schema: "GameDeals",
                table: "Posts",
                column: "FeedId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Posts",
                schema: "GameDeals");

            migrationBuilder.DropTable(
                name: "Feeds",
                schema: "GameDeals");

            migrationBuilder.DropTable(
                name: "Categories",
                schema: "GameDeals");
        }
    }
}
