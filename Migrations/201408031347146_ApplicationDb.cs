namespace SocialNetwork.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ApplicationDb : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CategoryModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CategoryName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.UserTaskModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserTaskTitle = c.String(),
                        UserTaskContent = c.String(),
                        UserTaskStatus = c.Boolean(nullable: false),
                        DateAdded = c.DateTime(nullable: false),
                        UserId = c.Int(nullable: false),
                        CategoryId = c.Int(nullable: false),
                        User_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CategoryModels", t => t.CategoryId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id)
                .Index(t => t.CategoryId)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.SolutionModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Solution = c.String(),
                        UserTaskId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.UserTaskModels", t => t.UserTaskId, cascadeDelete: true)
                .Index(t => t.UserTaskId);
            
            CreateTable(
                "dbo.CommentModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CommentContent = c.String(),
                        DateAdded = c.DateTime(nullable: false),
                        UserId = c.Int(nullable: false),
                        UserTaskId = c.Int(nullable: false),
                        User_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id)
                .ForeignKey("dbo.UserTaskModels", t => t.UserTaskId, cascadeDelete: true)
                .Index(t => t.UserTaskId)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.LikeModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        LikeValue = c.Boolean(nullable: false),
                        UserId = c.Int(nullable: false),
                        UserTaskId = c.Int(nullable: false),
                        User_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id)
                .ForeignKey("dbo.UserTaskModels", t => t.UserTaskId, cascadeDelete: true)
                .Index(t => t.UserTaskId)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.UserProposedSolutionModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProposedSolution = c.String(),
                        UserId = c.Int(nullable: false),
                        UserTaskId = c.Int(nullable: false),
                        User_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id)
                .ForeignKey("dbo.UserTaskModels", t => t.UserTaskId, cascadeDelete: true)
                .Index(t => t.UserTaskId)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.TagModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TagName = c.String(),
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
                .ForeignKey("dbo.TagModels", t => t.TagModel_Id, cascadeDelete: true)
                .ForeignKey("dbo.UserTaskModels", t => t.UserTaskModel_Id, cascadeDelete: true)
                .Index(t => t.TagModel_Id)
                .Index(t => t.UserTaskModel_Id);
            
            AddColumn("dbo.AspNetUsers", "UserRate", c => c.Int(nullable: false));
            AddColumn("dbo.AspNetUsers", "AttemptAmount", c => c.Int(nullable: false));
            AddColumn("dbo.AspNetUsers", "SolutionAmount", c => c.Int(nullable: false));
            AddColumn("dbo.AspNetUsers", "TaskAmount", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TagModelUserTaskModels", "UserTaskModel_Id", "dbo.UserTaskModels");
            DropForeignKey("dbo.TagModelUserTaskModels", "TagModel_Id", "dbo.TagModels");
            DropForeignKey("dbo.CommentModels", "UserTaskId", "dbo.UserTaskModels");
            DropForeignKey("dbo.UserTaskModels", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.UserProposedSolutionModels", "UserTaskId", "dbo.UserTaskModels");
            DropForeignKey("dbo.UserProposedSolutionModels", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.LikeModels", "UserTaskId", "dbo.UserTaskModels");
            DropForeignKey("dbo.LikeModels", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.CommentModels", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.UserTaskModels", "CategoryId", "dbo.CategoryModels");
            DropForeignKey("dbo.SolutionModels", "UserTaskId", "dbo.UserTaskModels");
            DropIndex("dbo.TagModelUserTaskModels", new[] { "UserTaskModel_Id" });
            DropIndex("dbo.TagModelUserTaskModels", new[] { "TagModel_Id" });
            DropIndex("dbo.UserProposedSolutionModels", new[] { "User_Id" });
            DropIndex("dbo.UserProposedSolutionModels", new[] { "UserTaskId" });
            DropIndex("dbo.LikeModels", new[] { "User_Id" });
            DropIndex("dbo.LikeModels", new[] { "UserTaskId" });
            DropIndex("dbo.CommentModels", new[] { "User_Id" });
            DropIndex("dbo.CommentModels", new[] { "UserTaskId" });
            DropIndex("dbo.SolutionModels", new[] { "UserTaskId" });
            DropIndex("dbo.UserTaskModels", new[] { "User_Id" });
            DropIndex("dbo.UserTaskModels", new[] { "CategoryId" });
            DropColumn("dbo.AspNetUsers", "TaskAmount");
            DropColumn("dbo.AspNetUsers", "SolutionAmount");
            DropColumn("dbo.AspNetUsers", "AttemptAmount");
            DropColumn("dbo.AspNetUsers", "UserRate");
            DropTable("dbo.TagModelUserTaskModels");
            DropTable("dbo.TagModels");
            DropTable("dbo.UserProposedSolutionModels");
            DropTable("dbo.LikeModels");
            DropTable("dbo.CommentModels");
            DropTable("dbo.SolutionModels");
            DropTable("dbo.UserTaskModels");
            DropTable("dbo.CategoryModels");
        }
    }
}
