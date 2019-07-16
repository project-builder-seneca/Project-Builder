namespace Project_Builder_Development.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Corrections1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ReplyReplies", "ReplyIdd", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ReplyReplies", "ReplyIdd");
        }
    }
}
