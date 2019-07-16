namespace Project_Builder_Development.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test1 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Ideas", "test");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Ideas", "test", c => c.Int(nullable: false));
        }
    }
}
