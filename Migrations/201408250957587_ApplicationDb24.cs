namespace SocialNetwork.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ApplicationDb24 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.UserTasks", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.UserTasks", new[] { "UserId" });
            AlterColumn("dbo.UserTasks", "UserId", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.UserTasks", "UserId");
            AddForeignKey("dbo.UserTasks", "UserId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserTasks", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.UserTasks", new[] { "UserId" });
            AlterColumn("dbo.UserTasks", "UserId", c => c.String(maxLength: 128));
            CreateIndex("dbo.UserTasks", "UserId");
            AddForeignKey("dbo.UserTasks", "UserId", "dbo.AspNetUsers", "Id");
        }
    }
}
