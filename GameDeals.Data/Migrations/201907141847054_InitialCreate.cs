namespace GameDeals.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "GameDeals.Categories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Value = c.String(maxLength: 30),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "GameDeals.Feeds",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CategoryId = c.Int(nullable: false),
                        Title = c.String(maxLength: 255),
                        Url = c.String(maxLength: 255),
                        FeedUrl = c.String(maxLength: 255),
                        LogoFileName = c.String(maxLength: 60),
                        DaysToExpire = c.Int(nullable: false),
                        ConsecutiveErroredFetches = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("GameDeals.Categories", t => t.CategoryId, cascadeDelete: true)
                .Index(t => t.CategoryId);
            
            CreateTable(
                "GameDeals.Posts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FeedId = c.Int(nullable: false),
                        Title = c.String(maxLength: 1024),
                        Url = c.String(maxLength: 1024),
                        PublishedAt = c.DateTime(nullable: false),
                        FetchedAt = c.DateTime(nullable: false),
                        Summary = c.String(),
                        Saved = c.Boolean(nullable: false),
                        Deleted = c.Boolean(nullable: false),
                        Seen = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("GameDeals.Feeds", t => t.FeedId, cascadeDelete: true)
                .Index(t => t.FeedId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("GameDeals.Posts", "FeedId", "GameDeals.Feeds");
            DropForeignKey("GameDeals.Feeds", "CategoryId", "GameDeals.Categories");
            DropIndex("GameDeals.Posts", new[] { "FeedId" });
            DropIndex("GameDeals.Feeds", new[] { "CategoryId" });
            DropTable("GameDeals.Posts");
            DropTable("GameDeals.Feeds");
            DropTable("GameDeals.Categories");
        }
    }
}
