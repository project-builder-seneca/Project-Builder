namespace Project_Builder_Development.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserNamemultiplerelationshipadded : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.TaskGivenUserNames", newName: "UserNameTaskGivens");
            RenameTable(name: "dbo.SkillTaskGivens", newName: "TaskGivenSkills");
            DropForeignKey("dbo.Ideas", "UserName_UserId", "dbo.UserNames");
            DropForeignKey("dbo.Ideas", "UserName_UserId1", "dbo.UserNames");
            DropForeignKey("dbo.Ideas", "UserName_UserId2", "dbo.UserNames");
            DropForeignKey("dbo.UserNames", "Idea_IdeaId", "dbo.Ideas");
            DropForeignKey("dbo.UserNames", "Idea_IdeaId1", "dbo.Ideas");
            DropForeignKey("dbo.UserNames", "Idea_IdeaId2", "dbo.Ideas");
            DropIndex("dbo.Ideas", new[] { "UserName_UserId" });
            DropIndex("dbo.Ideas", new[] { "UserName_UserId1" });
            DropIndex("dbo.Ideas", new[] { "UserName_UserId2" });
            DropIndex("dbo.UserNames", new[] { "Idea_IdeaId" });
            DropIndex("dbo.UserNames", new[] { "Idea_IdeaId1" });
            DropIndex("dbo.UserNames", new[] { "Idea_IdeaId2" });
            DropPrimaryKey("dbo.UserNameTaskGivens");
            DropPrimaryKey("dbo.TaskGivenSkills");
            CreateTable(
                "dbo.UserNameIdeas",
                c => new
                    {
                        UserName_UserId = c.Int(nullable: false),
                        Idea_IdeaId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserName_UserId, t.Idea_IdeaId })
                .ForeignKey("dbo.UserNames", t => t.UserName_UserId, cascadeDelete: true)
                .ForeignKey("dbo.Ideas", t => t.Idea_IdeaId, cascadeDelete: true)
                .Index(t => t.UserName_UserId)
                .Index(t => t.Idea_IdeaId);
            
            AddColumn("dbo.UserNames", "Patner", c => c.Boolean(nullable: false));
            AddColumn("dbo.UserNames", "Volunteer", c => c.Boolean(nullable: false));
            AddColumn("dbo.UserNames", "Investor", c => c.Boolean(nullable: false));
            AddPrimaryKey("dbo.UserNameTaskGivens", new[] { "UserName_UserId", "TaskGiven_TaskId" });
            AddPrimaryKey("dbo.TaskGivenSkills", new[] { "TaskGiven_TaskId", "Skill_SkillId" });
            DropColumn("dbo.Ideas", "UserName_UserId");
            DropColumn("dbo.Ideas", "UserName_UserId1");
            DropColumn("dbo.Ideas", "UserName_UserId2");
            DropColumn("dbo.UserNames", "Idea_IdeaId");
            DropColumn("dbo.UserNames", "Idea_IdeaId1");
            DropColumn("dbo.UserNames", "Idea_IdeaId2");
        }
        
        public override void Down()
        {
            AddColumn("dbo.UserNames", "Idea_IdeaId2", c => c.Int());
            AddColumn("dbo.UserNames", "Idea_IdeaId1", c => c.Int());
            AddColumn("dbo.UserNames", "Idea_IdeaId", c => c.Int());
            AddColumn("dbo.Ideas", "UserName_UserId2", c => c.Int());
            AddColumn("dbo.Ideas", "UserName_UserId1", c => c.Int());
            AddColumn("dbo.Ideas", "UserName_UserId", c => c.Int());
            DropForeignKey("dbo.UserNameIdeas", "Idea_IdeaId", "dbo.Ideas");
            DropForeignKey("dbo.UserNameIdeas", "UserName_UserId", "dbo.UserNames");
            DropIndex("dbo.UserNameIdeas", new[] { "Idea_IdeaId" });
            DropIndex("dbo.UserNameIdeas", new[] { "UserName_UserId" });
            DropPrimaryKey("dbo.TaskGivenSkills");
            DropPrimaryKey("dbo.UserNameTaskGivens");
            DropColumn("dbo.UserNames", "Investor");
            DropColumn("dbo.UserNames", "Volunteer");
            DropColumn("dbo.UserNames", "Patner");
            DropTable("dbo.UserNameIdeas");
            AddPrimaryKey("dbo.TaskGivenSkills", new[] { "Skill_SkillId", "TaskGiven_TaskId" });
            AddPrimaryKey("dbo.UserNameTaskGivens", new[] { "TaskGiven_TaskId", "UserName_UserId" });
            CreateIndex("dbo.UserNames", "Idea_IdeaId2");
            CreateIndex("dbo.UserNames", "Idea_IdeaId1");
            CreateIndex("dbo.UserNames", "Idea_IdeaId");
            CreateIndex("dbo.Ideas", "UserName_UserId2");
            CreateIndex("dbo.Ideas", "UserName_UserId1");
            CreateIndex("dbo.Ideas", "UserName_UserId");
            AddForeignKey("dbo.UserNames", "Idea_IdeaId2", "dbo.Ideas", "IdeaId");
            AddForeignKey("dbo.UserNames", "Idea_IdeaId1", "dbo.Ideas", "IdeaId");
            AddForeignKey("dbo.UserNames", "Idea_IdeaId", "dbo.Ideas", "IdeaId");
            AddForeignKey("dbo.Ideas", "UserName_UserId2", "dbo.UserNames", "UserId");
            AddForeignKey("dbo.Ideas", "UserName_UserId1", "dbo.UserNames", "UserId");
            AddForeignKey("dbo.Ideas", "UserName_UserId", "dbo.UserNames", "UserId");
            RenameTable(name: "dbo.TaskGivenSkills", newName: "SkillTaskGivens");
            RenameTable(name: "dbo.UserNameTaskGivens", newName: "TaskGivenUserNames");
        }
    }
}
