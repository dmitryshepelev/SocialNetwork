namespace SocialNetwork.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ApplicationDb13 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UsrTaskTags",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TagId = c.Int(nullable: false),
                        UserTaskId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Tags", t => t.TagId, cascadeDelete: true)
                .ForeignKey("dbo.UserTasks", t => t.UserTaskId, cascadeDelete: true)
                .Index(t => t.TagId)
                .Index(t => t.UserTaskId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UsrTaskTags", "UserTaskId", "dbo.UserTasks");
            DropForeignKey("dbo.UsrTaskTags", "TagId", "dbo.Tags");
            DropIndex("dbo.UsrTaskTags", new[] { "UserTaskId" });
            DropIndex("dbo.UsrTaskTags", new[] { "TagId" });
            DropTable("dbo.UsrTaskTags");
        }
    }
}
