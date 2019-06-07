namespace Project_Builder_Development.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Idea_Class_Change : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Categories", "Idea_IdeaId", "dbo.Ideas");
            DropIndex("dbo.Categories", new[] { "Idea_IdeaId" });
            AddColumn("dbo.Ideas", "Category", c => c.String(nullable: false));
            DropColumn("dbo.Categories", "Idea_IdeaId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Categories", "Idea_IdeaId", c => c.Int());
            DropColumn("dbo.Ideas", "Category");
            CreateIndex("dbo.Categories", "Idea_IdeaId");
            AddForeignKey("dbo.Categories", "Idea_IdeaId", "dbo.Ideas", "IdeaId");
        }
    }
}
