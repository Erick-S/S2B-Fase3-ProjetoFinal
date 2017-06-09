namespace Mobius.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserExtension1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "Rating", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "Rating");
        }
    }
}
