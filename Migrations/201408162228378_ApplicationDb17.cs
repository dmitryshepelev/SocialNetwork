namespace SocialNetwork.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ApplicationDb17 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.UserTasks", "AttemptsAmount");
        }
        
        public override void Down()
        {
            AddColumn("dbo.UserTasks", "AttemptsAmount", c => c.Int(nullable: false));
        }
    }
}
