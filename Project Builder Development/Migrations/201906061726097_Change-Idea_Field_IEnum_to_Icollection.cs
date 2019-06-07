namespace Project_Builder_Development.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeIdea_Field_IEnum_to_Icollection : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Categories", "Idea_IdeaId", c => c.Int());
            AddColumn("dbo.Ideas", "Owner", c => c.String(nullable: false));
            AddColumn("dbo.Skills", "Idea_IdeaId", c => c.Int());
            AddColumn("dbo.Skills", "Idea_IdeaId1", c => c.Int());
            CreateIndex("dbo.Categories", "Idea_IdeaId");
            CreateIndex("dbo.Skills", "Idea_IdeaId");
            CreateIndex("dbo.Skills", "Idea_IdeaId1");
            AddForeignKey("dbo.Categories", "Idea_IdeaId", "dbo.Ideas", "IdeaId");
            AddForeignKey("dbo.Skills", "Idea_IdeaId", "dbo.Ideas", "IdeaId");
            AddForeignKey("dbo.Skills", "Idea_IdeaId1", "dbo.Ideas", "IdeaId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Skills", "Idea_IdeaId1", "dbo.Ideas");
            DropForeignKey("dbo.Skills", "Idea_IdeaId", "dbo.Ideas");
            DropForeignKey("dbo.Categories", "Idea_IdeaId", "dbo.Ideas");
            DropIndex("dbo.Skills", new[] { "Idea_IdeaId1" });
            DropIndex("dbo.Skills", new[] { "Idea_IdeaId" });
            DropIndex("dbo.Categories", new[] { "Idea_IdeaId" });
            DropColumn("dbo.Skills", "Idea_IdeaId1");
            DropColumn("dbo.Skills", "Idea_IdeaId");
            DropColumn("dbo.Ideas", "Owner");
            DropColumn("dbo.Categories", "Idea_IdeaId");
        }
    }
}
