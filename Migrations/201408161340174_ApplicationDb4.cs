namespace SocialNetwork.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ApplicationDb4 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.TaskSolutions", "UserTaskId", "dbo.UserTasks");
            DropForeignKey("dbo.UserTasks", "CategoryId", "dbo.Categories");
            DropForeignKey("dbo.Comments", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Likes", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Likes", "UserTaskId", "dbo.UserTasks");
            DropForeignKey("dbo.UserSolvedTasks", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.UserSolvedTasks", "UserTaskId", "dbo.UserTasks");
            DropForeignKey("dbo.UserTasks", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Comments", "UserTaskId", "dbo.UserTasks");
            DropForeignKey("dbo.TagModelUserTaskModels", "TagModel_Id", "dbo.Tags");
            DropForeignKey("dbo.TagModelUserTaskModels", "UserTaskModel_Id", "dbo.UserTasks");
            DropIndex("dbo.UserTasks", new[] { "UserId" });
            DropIndex("dbo.UserTasks", new[] { "CategoryId" });
            DropIndex("dbo.TaskSolutions", new[] { "UserTaskId" });
            DropIndex("dbo.Comments", new[] { "UserId" });
            DropIndex("dbo.Comments", new[] { "UserTaskId" });
            DropIndex("dbo.Likes", new[] { "UserId" });
            DropIndex("dbo.Likes", new[] { "UserTaskId" });
            DropIndex("dbo.UserSolvedTasks", new[] { "UserId" });
            DropIndex("dbo.UserSolvedTasks", new[] { "UserTaskId" });
            DropIndex("dbo.TagModelUserTaskModels", new[] { "TagModel_Id" });
            DropIndex("dbo.TagModelUserTaskModels", new[] { "UserTaskModel_Id" });
            DropColumn("dbo.UserTasks", "UserId");
            DropColumn("dbo.UserTasks", "CategoryId");
            DropColumn("dbo.TaskSolutions", "UserTaskId");
            DropColumn("dbo.Comments", "UserId");
            DropColumn("dbo.Comments", "UserTaskId");
            DropColumn("dbo.Likes", "UserId");
            DropColumn("dbo.Likes", "UserTaskId");
            DropColumn("dbo.UserSolvedTasks", "UserId");
            DropColumn("dbo.UserSolvedTasks", "UserTaskId");
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
            
            AddColumn("dbo.UserSolvedTasks", "UserTaskId", c => c.Int(nullable: false));
            AddColumn("dbo.UserSolvedTasks", "UserId", c => c.String(maxLength: 128));
            AddColumn("dbo.Likes", "UserTaskId", c => c.Int(nullable: false));
            AddColumn("dbo.Likes", "UserId", c => c.String(maxLength: 128));
            AddColumn("dbo.Comments", "UserTaskId", c => c.Int(nullable: false));
            AddColumn("dbo.Comments", "UserId", c => c.String(maxLength: 128));
            AddColumn("dbo.TaskSolutions", "UserTaskId", c => c.Int(nullable: false));
            AddColumn("dbo.UserTasks", "CategoryId", c => c.Int(nullable: false));
            AddColumn("dbo.UserTasks", "UserId", c => c.String(maxLength: 128));
            CreateIndex("dbo.TagModelUserTaskModels", "UserTaskModel_Id");
            CreateIndex("dbo.TagModelUserTaskModels", "TagModel_Id");
            CreateIndex("dbo.UserSolvedTasks", "UserTaskId");
            CreateIndex("dbo.UserSolvedTasks", "UserId");
            CreateIndex("dbo.Likes", "UserTaskId");
            CreateIndex("dbo.Likes", "UserId");
            CreateIndex("dbo.Comments", "UserTaskId");
            CreateIndex("dbo.Comments", "UserId");
            CreateIndex("dbo.TaskSolutions", "UserTaskId");
            CreateIndex("dbo.UserTasks", "CategoryId");
            CreateIndex("dbo.UserTasks", "UserId");
            AddForeignKey("dbo.TagModelUserTaskModels", "UserTaskModel_Id", "dbo.UserTasks", "Id", cascadeDelete: true);
            AddForeignKey("dbo.TagModelUserTaskModels", "TagModel_Id", "dbo.Tags", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Comments", "UserTaskId", "dbo.UserTasks", "Id", cascadeDelete: true);
            AddForeignKey("dbo.UserTasks", "UserId", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.UserSolvedTasks", "UserTaskId", "dbo.UserTasks", "Id", cascadeDelete: true);
            AddForeignKey("dbo.UserSolvedTasks", "UserId", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Likes", "UserTaskId", "dbo.UserTasks", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Likes", "UserId", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Comments", "UserId", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.UserTasks", "CategoryId", "dbo.Categories", "Id", cascadeDelete: true);
            AddForeignKey("dbo.TaskSolutions", "UserTaskId", "dbo.UserTasks", "Id", cascadeDelete: true);
        }
    }
}
