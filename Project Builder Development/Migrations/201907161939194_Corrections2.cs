namespace Project_Builder_Development.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Corrections2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ReplyReplies", "IdeaId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ReplyReplies", "IdeaId");
        }
    }
}
