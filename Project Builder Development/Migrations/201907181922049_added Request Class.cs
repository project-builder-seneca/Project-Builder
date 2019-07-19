namespace Project_Builder_Development.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedRequestClass : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Requests",
                c => new
                    {
                        RequestId = c.Int(nullable: false, identity: true),
                        Message = c.String(),
                        Ideas_IdeaId = c.Int(nullable: false),
                        UserNames_UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.RequestId)
                .ForeignKey("dbo.Ideas", t => t.Ideas_IdeaId)
                .ForeignKey("dbo.UserNames", t => t.UserNames_UserId)
                .Index(t => t.Ideas_IdeaId)
                .Index(t => t.UserNames_UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Requests", "UserNames_UserId", "dbo.UserNames");
            DropForeignKey("dbo.Requests", "Ideas_IdeaId", "dbo.Ideas");
            DropIndex("dbo.Requests", new[] { "UserNames_UserId" });
            DropIndex("dbo.Requests", new[] { "Ideas_IdeaId" });
            DropTable("dbo.Requests");
        }
    }
}
