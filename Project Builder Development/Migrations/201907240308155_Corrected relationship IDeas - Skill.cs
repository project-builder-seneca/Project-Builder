namespace Project_Builder_Development.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CorrectedrelationshipIDeasSkill : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Ideas", "Skill_SkillId", "dbo.Skills");
            DropForeignKey("dbo.Ideas", "Skill_SkillId1", "dbo.Skills");
            DropForeignKey("dbo.Skills", "Idea_IdeaId", "dbo.Ideas");
            DropForeignKey("dbo.Skills", "Idea_IdeaId1", "dbo.Ideas");
            DropIndex("dbo.Ideas", new[] { "Skill_SkillId" });
            DropIndex("dbo.Ideas", new[] { "Skill_SkillId1" });
            DropIndex("dbo.Skills", new[] { "Idea_IdeaId" });
            DropIndex("dbo.Skills", new[] { "Idea_IdeaId1" });
            CreateTable(
                "dbo.SkillIdeas",
                c => new
                    {
                        Skill_SkillId = c.Int(nullable: false),
                        Idea_IdeaId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Skill_SkillId, t.Idea_IdeaId })
                .ForeignKey("dbo.Skills", t => t.Skill_SkillId, cascadeDelete: true)
                .ForeignKey("dbo.Ideas", t => t.Idea_IdeaId, cascadeDelete: true)
                .Index(t => t.Skill_SkillId)
                .Index(t => t.Idea_IdeaId);
            
            AddColumn("dbo.Skills", "Patner", c => c.Boolean(nullable: false));
            AddColumn("dbo.Skills", "Volunteer", c => c.Boolean(nullable: false));
            DropColumn("dbo.Ideas", "Skill_SkillId");
            DropColumn("dbo.Ideas", "Skill_SkillId1");
            DropColumn("dbo.Skills", "Idea_IdeaId");
            DropColumn("dbo.Skills", "Idea_IdeaId1");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Skills", "Idea_IdeaId1", c => c.Int());
            AddColumn("dbo.Skills", "Idea_IdeaId", c => c.Int());
            AddColumn("dbo.Ideas", "Skill_SkillId1", c => c.Int());
            AddColumn("dbo.Ideas", "Skill_SkillId", c => c.Int());
            DropForeignKey("dbo.SkillIdeas", "Idea_IdeaId", "dbo.Ideas");
            DropForeignKey("dbo.SkillIdeas", "Skill_SkillId", "dbo.Skills");
            DropIndex("dbo.SkillIdeas", new[] { "Idea_IdeaId" });
            DropIndex("dbo.SkillIdeas", new[] { "Skill_SkillId" });
            DropColumn("dbo.Skills", "Volunteer");
            DropColumn("dbo.Skills", "Patner");
            DropTable("dbo.SkillIdeas");
            CreateIndex("dbo.Skills", "Idea_IdeaId1");
            CreateIndex("dbo.Skills", "Idea_IdeaId");
            CreateIndex("dbo.Ideas", "Skill_SkillId1");
            CreateIndex("dbo.Ideas", "Skill_SkillId");
            AddForeignKey("dbo.Skills", "Idea_IdeaId1", "dbo.Ideas", "IdeaId");
            AddForeignKey("dbo.Skills", "Idea_IdeaId", "dbo.Ideas", "IdeaId");
            AddForeignKey("dbo.Ideas", "Skill_SkillId1", "dbo.Skills", "SkillId");
            AddForeignKey("dbo.Ideas", "Skill_SkillId", "dbo.Skills", "SkillId");
        }
    }
}
