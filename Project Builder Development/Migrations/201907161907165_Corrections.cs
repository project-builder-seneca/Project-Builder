namespace Project_Builder_Development.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Corrections : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Replies", name: "Idea_IdeaId", newName: "IdeaId");
            RenameIndex(table: "dbo.Replies", name: "IX_Idea_IdeaId", newName: "IX_IdeaId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Replies", name: "IX_IdeaId", newName: "IX_Idea_IdeaId");
            RenameColumn(table: "dbo.Replies", name: "IdeaId", newName: "Idea_IdeaId");
        }
    }
}
