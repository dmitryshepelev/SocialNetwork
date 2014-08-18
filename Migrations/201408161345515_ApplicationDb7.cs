namespace SocialNetwork.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ApplicationDb7 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Tags", "UserTaskModel_Id", "dbo.UserTasks");
            DropIndex("dbo.Tags", new[] { "UserTaskModel_Id" });
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
            
            DropColumn("dbo.Tags", "UserTaskModel_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Tags", "UserTaskModel_Id", c => c.Int());
            DropForeignKey("dbo.TagModelUserTaskModels", "UserTaskModel_Id", "dbo.UserTasks");
            DropForeignKey("dbo.TagModelUserTaskModels", "TagModel_Id", "dbo.Tags");
            DropIndex("dbo.TagModelUserTaskModels", new[] { "UserTaskModel_Id" });
            DropIndex("dbo.TagModelUserTaskModels", new[] { "TagModel_Id" });
            DropTable("dbo.TagModelUserTaskModels");
            CreateIndex("dbo.Tags", "UserTaskModel_Id");
            AddForeignKey("dbo.Tags", "UserTaskModel_Id", "dbo.UserTasks", "Id");
        }
    }
}
