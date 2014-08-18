namespace SocialNetwork.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ApplicationDb6 : DbMigration
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
                        Category_Id = c.Int(),
                        User_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Categories", t => t.Category_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id)
                .Index(t => t.Category_Id)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.TaskSolutions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Solution = c.String(),
                        UserTask_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.UserTasks", t => t.UserTask_Id)
                .Index(t => t.UserTask_Id);
            
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CommentContent = c.String(),
                        DateAdded = c.DateTime(nullable: false),
                        User_Id = c.String(maxLength: 128),
                        UserTask_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id)
                .ForeignKey("dbo.UserTasks", t => t.UserTask_Id)
                .Index(t => t.User_Id)
                .Index(t => t.UserTask_Id);
            
            CreateTable(
                "dbo.Likes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        LikeValue = c.Boolean(nullable: false),
                        User_Id = c.String(maxLength: 128),
                        UserTask_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id)
                .ForeignKey("dbo.UserTasks", t => t.UserTask_Id)
                .Index(t => t.User_Id)
                .Index(t => t.UserTask_Id);
            
            CreateTable(
                "dbo.UserSolvedTasks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        User_Id = c.String(maxLength: 128),
                        UserTask_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id)
                .ForeignKey("dbo.UserTasks", t => t.UserTask_Id)
                .Index(t => t.User_Id)
                .Index(t => t.UserTask_Id);
            
            CreateTable(
                "dbo.Tags",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TagName = c.String(),
                        TagFrequency = c.Int(nullable: false),
                        UserTaskModel_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.UserTasks", t => t.UserTaskModel_Id)
                .Index(t => t.UserTaskModel_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tags", "UserTaskModel_Id", "dbo.UserTasks");
            DropForeignKey("dbo.Comments", "UserTask_Id", "dbo.UserTasks");
            DropForeignKey("dbo.UserTasks", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.UserSolvedTasks", "UserTask_Id", "dbo.UserTasks");
            DropForeignKey("dbo.UserSolvedTasks", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Likes", "UserTask_Id", "dbo.UserTasks");
            DropForeignKey("dbo.Likes", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Comments", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.UserTasks", "Category_Id", "dbo.Categories");
            DropForeignKey("dbo.TaskSolutions", "UserTask_Id", "dbo.UserTasks");
            DropIndex("dbo.Tags", new[] { "UserTaskModel_Id" });
            DropIndex("dbo.UserSolvedTasks", new[] { "UserTask_Id" });
            DropIndex("dbo.UserSolvedTasks", new[] { "User_Id" });
            DropIndex("dbo.Likes", new[] { "UserTask_Id" });
            DropIndex("dbo.Likes", new[] { "User_Id" });
            DropIndex("dbo.Comments", new[] { "UserTask_Id" });
            DropIndex("dbo.Comments", new[] { "User_Id" });
            DropIndex("dbo.TaskSolutions", new[] { "UserTask_Id" });
            DropIndex("dbo.UserTasks", new[] { "User_Id" });
            DropIndex("dbo.UserTasks", new[] { "Category_Id" });
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
