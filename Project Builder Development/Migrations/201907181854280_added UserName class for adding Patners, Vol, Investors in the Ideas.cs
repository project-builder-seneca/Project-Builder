namespace Project_Builder_Development.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedUserNameclassforaddingPatnersVolInvestorsintheIdeas : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserNames",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Idea_IdeaId = c.Int(),
                        Idea_IdeaId1 = c.Int(),
                        Idea_IdeaId2 = c.Int(),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.Ideas", t => t.Idea_IdeaId)
                .ForeignKey("dbo.Ideas", t => t.Idea_IdeaId1)
                .ForeignKey("dbo.Ideas", t => t.Idea_IdeaId2)
                .Index(t => t.Idea_IdeaId)
                .Index(t => t.Idea_IdeaId1)
                .Index(t => t.Idea_IdeaId2);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserNames", "Idea_IdeaId2", "dbo.Ideas");
            DropForeignKey("dbo.UserNames", "Idea_IdeaId1", "dbo.Ideas");
            DropForeignKey("dbo.UserNames", "Idea_IdeaId", "dbo.Ideas");
            DropIndex("dbo.UserNames", new[] { "Idea_IdeaId2" });
            DropIndex("dbo.UserNames", new[] { "Idea_IdeaId1" });
            DropIndex("dbo.UserNames", new[] { "Idea_IdeaId" });
            DropTable("dbo.UserNames");
        }
    }
}
