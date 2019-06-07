namespace Project_Builder_Development.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Idea_Class_Change_1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Ideas", "Category_CategoryId", c => c.Int(nullable: false));
            CreateIndex("dbo.Ideas", "Category_CategoryId");
            AddForeignKey("dbo.Ideas", "Category_CategoryId", "dbo.Categories", "CategoryId");
            DropColumn("dbo.Ideas", "Category");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Ideas", "Category", c => c.String(nullable: false));
            DropForeignKey("dbo.Ideas", "Category_CategoryId", "dbo.Categories");
            DropIndex("dbo.Ideas", new[] { "Category_CategoryId" });
            DropColumn("dbo.Ideas", "Category_CategoryId");
        }
    }
}
