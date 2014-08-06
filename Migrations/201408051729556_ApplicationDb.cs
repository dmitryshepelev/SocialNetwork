namespace SocialNetwork.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ApplicationDb : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CategoryName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.UserTasks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserTaskTitle = c.String(),
                        UserTaskContent = c.String(),
                        UserTaskStatus = c.Boolean(nullable: false),
                        DateAdded = c.DateTime(nullable: false),
                        UserId = c.String(maxLength: 128),
                        CategoryId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Categories", t => t.CategoryId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.CategoryId);
            
            CreateTable(
                "dbo.TaskSolutions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Solution = c.String(),
                        UserTaskId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.UserTasks", t => t.UserTaskId, cascadeDelete: true)
                .Index(t => t.UserTaskId);
            
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CommentContent = c.String(),
                        DateAdded = c.DateTime(nullable: false),
                        UserId = c.String(maxLength: 128),
                        UserTaskId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .ForeignKey("dbo.UserTasks", t => t.UserTaskId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.UserTaskId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        UserRate = c.Double(nullable: false),
                        AttemptAmount = c.Int(nullable: false),
                        UserPhotoUrl = c.String(),
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
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Likes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        LikeValue = c.Boolean(nullable: false),
                        UserId = c.String(maxLength: 128),
                        UserTaskId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .ForeignKey("dbo.UserTasks", t => t.UserTaskId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.UserTaskId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.UserSolvedTasks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(maxLength: 128),
                        UserTaskId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .ForeignKey("dbo.UserTasks", t => t.UserTaskId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.UserTaskId);
            
            CreateTable(
                "dbo.Tags",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TagName = c.String(),
                        TagFrequency = c.Int(nullable: false),
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
                "dbo.TagModelUserTaskModels",
                c => new
                    {
                        TagModel_Id = c.Int(nullable: false),
                        UserTaskModel_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.TagModel_Id, t.UserTaskModel_Id })
                .ForeignKey("dbo.Tags", t => t.TagModel_Id, cascadeDelete: true)
                .ForeignKey("dbo.UserTasks", t => t.UserTaskModel_Id, cascadeDelete: true)
                .Index(t => t.TagModel_Id)
                .Index(t => t.UserTaskModel_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.TagModelUserTaskModels", "UserTaskModel_Id", "dbo.UserTasks");
            DropForeignKey("dbo.TagModelUserTaskModels", "TagModel_Id", "dbo.Tags");
            DropForeignKey("dbo.Comments", "UserTaskId", "dbo.UserTasks");
            DropForeignKey("dbo.UserTasks", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.UserSolvedTasks", "UserTaskId", "dbo.UserTasks");
            DropForeignKey("dbo.UserSolvedTasks", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Likes", "UserTaskId", "dbo.UserTasks");
            DropForeignKey("dbo.Likes", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Comments", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.UserTasks", "CategoryId", "dbo.Categories");
            DropForeignKey("dbo.TaskSolutions", "UserTaskId", "dbo.UserTasks");
            DropIndex("dbo.TagModelUserTaskModels", new[] { "UserTaskModel_Id" });
            DropIndex("dbo.TagModelUserTaskModels", new[] { "TagModel_Id" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.UserSolvedTasks", new[] { "UserTaskId" });
            DropIndex("dbo.UserSolvedTasks", new[] { "UserId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.Likes", new[] { "UserTaskId" });
            DropIndex("dbo.Likes", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.Comments", new[] { "UserTaskId" });
            DropIndex("dbo.Comments", new[] { "UserId" });
            DropIndex("dbo.TaskSolutions", new[] { "UserTaskId" });
            DropIndex("dbo.UserTasks", new[] { "CategoryId" });
            DropIndex("dbo.UserTasks", new[] { "UserId" });
            DropTable("dbo.TagModelUserTaskModels");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Tags");
            DropTable("dbo.UserSolvedTasks");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.Likes");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Comments");
            DropTable("dbo.TaskSolutions");
            DropTable("dbo.UserTasks");
            DropTable("dbo.Categories");
        }
    }
}
