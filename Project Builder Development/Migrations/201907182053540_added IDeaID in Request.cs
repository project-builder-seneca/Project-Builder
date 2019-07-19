namespace Project_Builder_Development.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedIDeaIDinRequest : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Requests", name: "Ideas_IdeaId", newName: "IdeaId");
            RenameIndex(table: "dbo.Requests", name: "IX_Ideas_IdeaId", newName: "IX_IdeaId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Requests", name: "IX_IdeaId", newName: "IX_Ideas_IdeaId");
            RenameColumn(table: "dbo.Requests", name: "IdeaId", newName: "Ideas_IdeaId");
        }
    }
}
