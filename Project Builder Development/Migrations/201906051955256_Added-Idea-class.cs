namespace Project_Builder_Development.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedIdeaclass : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Ideas",
                c => new
                    {
                        IdeaId = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 100),
                        Description = c.String(nullable: false, maxLength: 300),
                        InvestmentGoal = c.Int(nullable: false),
                        PartnersRequired = c.Int(nullable: false),
                        VolunteersRequired = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.IdeaId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Ideas");
        }
    }
}
