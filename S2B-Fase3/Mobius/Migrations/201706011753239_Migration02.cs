namespace Mobius.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migration02 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Products", "CategoryID", "dbo.Categories");
            DropForeignKey("dbo.Products", "UserID", "dbo.AspNetUsers");
            DropIndex("dbo.Products", new[] { "CategoryID" });
            DropIndex("dbo.Products", new[] { "UserID" });
            DropColumn("dbo.AspNetUsers", "Rating");
            DropTable("dbo.Products");
            DropTable("dbo.Categories");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        CategoryID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        Description = c.String(nullable: false, maxLength: 140),
                    })
                .PrimaryKey(t => t.CategoryID);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        ProductID = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 100),
                        Description = c.String(nullable: false, maxLength: 140),
                        Cost = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Address = c.String(nullable: false),
                        PublishDate = c.DateTime(nullable: false),
                        ExpirationDate = c.DateTime(nullable: false),
                        Status = c.Int(nullable: false),
                        Rating = c.Int(nullable: false),
                        ImageFile = c.Binary(),
                        ImageMimeType = c.String(),
                        ImageUrl = c.String(),
                        CategoryID = c.Int(nullable: false),
                        UserID = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ProductID);
            
            AddColumn("dbo.AspNetUsers", "Rating", c => c.Int(nullable: false));
            CreateIndex("dbo.Products", "UserID");
            CreateIndex("dbo.Products", "CategoryID");
            AddForeignKey("dbo.Products", "UserID", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Products", "CategoryID", "dbo.Categories", "CategoryID", cascadeDelete: true);
        }
    }
}
