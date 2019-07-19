namespace Project_Builder_Development.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedPatVolInvestinRequest : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Requests", "Patner", c => c.Boolean(nullable: false));
            AddColumn("dbo.Requests", "Volunteer", c => c.Boolean(nullable: false));
            AddColumn("dbo.Requests", "Investor", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Requests", "Investor");
            DropColumn("dbo.Requests", "Volunteer");
            DropColumn("dbo.Requests", "Patner");
        }
    }
}
