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
            
            AddColumn("dbo.AspNetUsers", "UserRate", c => c.Double(nullable: false));
            AddColumn("dbo.AspNetUsers", "AttemptAmount", c => c.Int(nullable: false));
            AddColumn("dbo.AspNetUsers", "UserPhotoUrl", c => c.String());
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TagModelUserTaskModels", "UserTaskModel_Id", "dbo.UserTasks");
            DropForeignKey("dbo.TagModelUserTaskModels", "TagModel_Id", "dbo.Tags");
            DropForeignKey("dbo.Comments", "UserTaskId", "dbo.UserTasks");
            DropForeignKey("dbo.UserTasks", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.UserSolvedTasks", "UserTaskId", "dbo.UserTasks");
            DropForeignKey("dbo.UserSolvedTasks", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Likes", "UserTaskId", "dbo.UserTasks");
            DropForeignKey("dbo.Likes", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Comments", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.UserTasks", "CategoryId", "dbo.Categories");
            DropForeignKey("dbo.TaskSolutions", "UserTaskId", "dbo.UserTasks");
            DropIndex("dbo.TagModelUserTaskModels", new[] { "UserTaskModel_Id" });
            DropIndex("dbo.TagModelUserTaskModels", new[] { "TagModel_Id" });
            DropIndex("dbo.UserSolvedTasks", new[] { "UserTaskId" });
            DropIndex("dbo.UserSolvedTasks", new[] { "UserId" });
            DropIndex("dbo.Likes", new[] { "UserTaskId" });
            DropIndex("dbo.Likes", new[] { "UserId" });
            DropIndex("dbo.Comments", new[] { "UserTaskId" });
            DropIndex("dbo.Comments", new[] { "UserId" });
            DropIndex("dbo.TaskSolutions", new[] { "UserTaskId" });
            DropIndex("dbo.UserTasks", new[] { "CategoryId" });
            DropIndex("dbo.UserTasks", new[] { "UserId" });
            DropColumn("dbo.AspNetUsers", "UserPhotoUrl");
            DropColumn("dbo.AspNetUsers", "AttemptAmount");
            DropColumn("dbo.AspNetUsers", "UserRate");
            DropTable("dbo.TagModelUserTaskModels");
            DropTable("dbo.Tags");
            DropTable("dbo.UserSolvedTasks");
            DropTable("dbo.Likes");
            DropTable("dbo.Comments");
            DropTable("dbo.TaskSolutions");
            DropTable("dbo.UserTasks");
            DropTable("dbo.Categories");
        }
    }
}
