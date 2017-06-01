namespace Mobius.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migration01 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Products", new[] { "User_Id" });
            DropColumn("dbo.Products", "UserID");
            RenameColumn(table: "dbo.Products", name: "User_Id", newName: "UserID");
            AlterColumn("dbo.Products", "UserID", c => c.String(maxLength: 128));
            CreateIndex("dbo.Products", "UserID");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Products", new[] { "UserID" });
            AlterColumn("dbo.Products", "UserID", c => c.Int(nullable: false));
            RenameColumn(table: "dbo.Products", name: "UserID", newName: "User_Id");
            AddColumn("dbo.Products", "UserID", c => c.Int(nullable: false));
            CreateIndex("dbo.Products", "User_Id");
        }
    }
}
