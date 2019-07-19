namespace Project_Builder_Development.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changecolumninRequesttable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Requests", "UserNames_UserId", "dbo.UserNames");
            DropIndex("dbo.Requests", new[] { "UserNames_UserId" });
            AddColumn("dbo.Requests", "UserName", c => c.String(nullable: false));
            DropColumn("dbo.Requests", "UserNames_UserId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Requests", "UserNames_UserId", c => c.Int(nullable: false));
            DropColumn("dbo.Requests", "UserName");
            CreateIndex("dbo.Requests", "UserNames_UserId");
            AddForeignKey("dbo.Requests", "UserNames_UserId", "dbo.UserNames", "UserId");
        }
    }
}
