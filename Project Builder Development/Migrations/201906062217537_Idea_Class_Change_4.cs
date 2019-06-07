namespace Project_Builder_Development.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Idea_Class_Change_4 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Ideas", "Skill_SkillId", c => c.Int());
            CreateIndex("dbo.Ideas", "Skill_SkillId");
            AddForeignKey("dbo.Ideas", "Skill_SkillId", "dbo.Skills", "SkillId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Ideas", "Skill_SkillId", "dbo.Skills");
            DropIndex("dbo.Ideas", new[] { "Skill_SkillId" });
            DropColumn("dbo.Ideas", "Skill_SkillId");
        }
    }
}
