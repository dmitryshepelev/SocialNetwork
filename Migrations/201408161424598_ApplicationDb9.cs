namespace SocialNetwork.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ApplicationDb9 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.TaskSolutions", "UserTask_Id", "dbo.UserTasks");
            DropForeignKey("dbo.Comments", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Likes", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Likes", "UserTask_Id", "dbo.UserTasks");
            DropForeignKey("dbo.UserSolvedTasks", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.UserSolvedTasks", "UserTask_Id", "dbo.UserTasks");
            DropForeignKey("dbo.UserTasks", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Comments", "UserTask_Id", "dbo.UserTasks");
            DropForeignKey("dbo.TagModelUserTaskModels", "TagModel_Id", "dbo.Tags");
            DropForeignKey("dbo.TagModelUserTaskModels", "UserTaskModel_Id", "dbo.UserTasks");
            DropForeignKey("dbo.UserTasks", "CategoryModel_Id", "dbo.Categories");
            DropIndex("dbo.UserTasks", new[] { "User_Id" });
            DropIndex("dbo.UserTasks", new[] { "CategoryModel_Id" });
            DropIndex("dbo.TaskSolutions", new[] { "UserTask_Id" });
            DropIndex("dbo.Comments", new[] { "User_Id" });
            DropIndex("dbo.Comments", new[] { "UserTask_Id" });
            DropIndex("dbo.Likes", new[] { "User_Id" });
            DropIndex("dbo.Likes", new[] { "UserTask_Id" });
            DropIndex("dbo.UserSolvedTasks", new[] { "User_Id" });
            DropIndex("dbo.UserSolvedTasks", new[] { "UserTask_Id" });
            DropIndex("dbo.TagModelUserTaskModels", new[] { "TagModel_Id" });
            DropIndex("dbo.TagModelUserTaskModels", new[] { "UserTaskModel_Id" });
            //DropTable("dbo.Categories");
            DropTable("dbo.UserTasks");
            DropTable("dbo.TaskSolutions");
            DropTable("dbo.Comments");
            DropTable("dbo.Likes");
            DropTable("dbo.UserSolvedTasks");
            DropTable("dbo.Tags");
            DropTable("dbo.TagModelUserTaskModels");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.TagModelUserTaskModels",
                c => new
                    {
                        TagModel_Id = c.Int(nullable: false),
                        UserTaskModel_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.TagModel_Id, t.UserTaskModel_Id });
            
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
                "dbo.UserSolvedTasks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        User_Id = c.String(maxLength: 128),
                        UserTask_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Likes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        LikeValue = c.Boolean(nullable: false),
                        User_Id = c.String(maxLength: 128),
                        UserTask_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id);
            
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
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TaskSolutions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Solution = c.String(),
                        UserTask_Id = c.Int(),
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
                        CategoryId = c.Int(nullable: false),
                        User_Id = c.String(maxLength: 128),
                        CategoryModel_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CategoryName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateIndex("dbo.TagModelUserTaskModels", "UserTaskModel_Id");
            CreateIndex("dbo.TagModelUserTaskModels", "TagModel_Id");
            CreateIndex("dbo.UserSolvedTasks", "UserTask_Id");
            CreateIndex("dbo.UserSolvedTasks", "User_Id");
            CreateIndex("dbo.Likes", "UserTask_Id");
            CreateIndex("dbo.Likes", "User_Id");
            CreateIndex("dbo.Comments", "UserTask_Id");
            CreateIndex("dbo.Comments", "User_Id");
            CreateIndex("dbo.TaskSolutions", "UserTask_Id");
            CreateIndex("dbo.UserTasks", "CategoryModel_Id");
            CreateIndex("dbo.UserTasks", "User_Id");
            AddForeignKey("dbo.UserTasks", "CategoryModel_Id", "dbo.Categories", "Id");
            AddForeignKey("dbo.TagModelUserTaskModels", "UserTaskModel_Id", "dbo.UserTasks", "Id", cascadeDelete: true);
            AddForeignKey("dbo.TagModelUserTaskModels", "TagModel_Id", "dbo.Tags", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Comments", "UserTask_Id", "dbo.UserTasks", "Id");
            AddForeignKey("dbo.UserTasks", "User_Id", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.UserSolvedTasks", "UserTask_Id", "dbo.UserTasks", "Id");
            AddForeignKey("dbo.UserSolvedTasks", "User_Id", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Likes", "UserTask_Id", "dbo.UserTasks", "Id");
            AddForeignKey("dbo.Likes", "User_Id", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Comments", "User_Id", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.TaskSolutions", "UserTask_Id", "dbo.UserTasks", "Id");
        }
    }
}
