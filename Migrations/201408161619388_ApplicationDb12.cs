namespace SocialNetwork.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ApplicationDb12 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.TagModelUserTaskModels", "TagModel_Id", "dbo.Tags");
            DropForeignKey("dbo.TagModelUserTaskModels", "UserTaskModel_Id", "dbo.UserTasks");
            DropIndex("dbo.TagModelUserTaskModels", new[] { "TagModel_Id" });
            DropIndex("dbo.TagModelUserTaskModels", new[] { "UserTaskModel_Id" });
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
            
            CreateIndex("dbo.TagModelUserTaskModels", "UserTaskModel_Id");
            CreateIndex("dbo.TagModelUserTaskModels", "TagModel_Id");
            AddForeignKey("dbo.TagModelUserTaskModels", "UserTaskModel_Id", "dbo.UserTasks", "Id", cascadeDelete: true);
            AddForeignKey("dbo.TagModelUserTaskModels", "TagModel_Id", "dbo.Tags", "Id", cascadeDelete: true);
        }
    }
}
