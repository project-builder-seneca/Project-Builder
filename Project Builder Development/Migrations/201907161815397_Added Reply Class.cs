namespace Project_Builder_Development.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedReplyClass : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Replies",
                c => new
                    {
                        ReplyId = c.Int(nullable: false, identity: true),
                        message = c.String(nullable: false),
                        UserName = c.String(nullable: false),
                        Idea_IdeaId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ReplyId)
                .ForeignKey("dbo.Ideas", t => t.Idea_IdeaId)
                .Index(t => t.Idea_IdeaId);
            
            CreateTable(
                "dbo.ReplyReplies",
                c => new
                    {
                        ReplyId = c.Int(nullable: false, identity: true),
                        message = c.String(nullable: false),
                        Reply_ReplyId = c.Int(),
                    })
                .PrimaryKey(t => t.ReplyId)
                .ForeignKey("dbo.Replies", t => t.Reply_ReplyId)
                .Index(t => t.Reply_ReplyId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ReplyReplies", "Reply_ReplyId", "dbo.Replies");
            DropForeignKey("dbo.Replies", "Idea_IdeaId", "dbo.Ideas");
            DropIndex("dbo.ReplyReplies", new[] { "Reply_ReplyId" });
            DropIndex("dbo.Replies", new[] { "Idea_IdeaId" });
            DropTable("dbo.ReplyReplies");
            DropTable("dbo.Replies");
        }
    }
}
