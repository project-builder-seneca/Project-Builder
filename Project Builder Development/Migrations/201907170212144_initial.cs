namespace Project_Builder_Development.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        CategoryId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.CategoryId);
            
            CreateTable(
                "dbo.Ideas",
                c => new
                    {
                        IdeaId = c.Int(nullable: false, identity: true),
                        Owner = c.String(nullable: false),
                        Title = c.String(nullable: false, maxLength: 100),
                        Description = c.String(nullable: false, maxLength: 300),
                        InvestmentGoal = c.Int(nullable: false),
                        PartnersRequired = c.Int(nullable: false),
                        VolunteersRequired = c.Int(nullable: false),
                        Like = c.Int(nullable: false),
                        Dislike = c.Int(nullable: false),
                        Category_CategoryId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.IdeaId)
                .ForeignKey("dbo.Categories", t => t.Category_CategoryId)
                .Index(t => t.Category_CategoryId);
            
            CreateTable(
                "dbo.Skills",
                c => new
                    {
                        SkillId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 25),
                        Idea_IdeaId = c.Int(),
                        Idea_IdeaId1 = c.Int(),
                    })
                .PrimaryKey(t => t.SkillId)
                .ForeignKey("dbo.Ideas", t => t.Idea_IdeaId)
                .ForeignKey("dbo.Ideas", t => t.Idea_IdeaId1)
                .Index(t => t.Idea_IdeaId)
                .Index(t => t.Idea_IdeaId1);
            
            CreateTable(
                "dbo.Reacts",
                c => new
                    {
                        ReactId = c.Int(nullable: false, identity: true),
                        user = c.String(nullable: false),
                        like = c.Boolean(nullable: false),
                        dislike = c.Boolean(nullable: false),
                        IdeasId = c.Int(nullable: false),
                        Idea_IdeaId = c.Int(),
                    })
                .PrimaryKey(t => t.ReactId)
                .ForeignKey("dbo.Ideas", t => t.Idea_IdeaId)
                .Index(t => t.Idea_IdeaId);
            
            CreateTable(
                "dbo.Replies",
                c => new
                    {
                        ReplyId = c.Int(nullable: false, identity: true),
                        message = c.String(nullable: false),
                        UserName = c.String(nullable: false),
                        IdeaId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ReplyId)
                .ForeignKey("dbo.Ideas", t => t.IdeaId)
                .Index(t => t.IdeaId);
            
            CreateTable(
                "dbo.ReplyReplies",
                c => new
                    {
                        ReplyId = c.Int(nullable: false, identity: true),
                        message = c.String(nullable: false),
                        ReplyIdd = c.Int(nullable: false),
                        IdeaId = c.Int(nullable: false),
                        Reply_ReplyId = c.Int(),
                    })
                .PrimaryKey(t => t.ReplyId)
                .ForeignKey("dbo.Replies", t => t.Reply_ReplyId)
                .Index(t => t.Reply_ReplyId);
            
            CreateTable(
                "dbo.RoleClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Skills", "Idea_IdeaId1", "dbo.Ideas");
            DropForeignKey("dbo.Replies", "IdeaId", "dbo.Ideas");
            DropForeignKey("dbo.ReplyReplies", "Reply_ReplyId", "dbo.Replies");
            DropForeignKey("dbo.Reacts", "Idea_IdeaId", "dbo.Ideas");
            DropForeignKey("dbo.Skills", "Idea_IdeaId", "dbo.Ideas");
            DropForeignKey("dbo.Ideas", "Category_CategoryId", "dbo.Categories");
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.ReplyReplies", new[] { "Reply_ReplyId" });
            DropIndex("dbo.Replies", new[] { "IdeaId" });
            DropIndex("dbo.Reacts", new[] { "Idea_IdeaId" });
            DropIndex("dbo.Skills", new[] { "Idea_IdeaId1" });
            DropIndex("dbo.Skills", new[] { "Idea_IdeaId" });
            DropIndex("dbo.Ideas", new[] { "Category_CategoryId" });
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.RoleClaims");
            DropTable("dbo.ReplyReplies");
            DropTable("dbo.Replies");
            DropTable("dbo.Reacts");
            DropTable("dbo.Skills");
            DropTable("dbo.Ideas");
            DropTable("dbo.Categories");
        }
    }
}
