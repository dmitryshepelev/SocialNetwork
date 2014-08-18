namespace SocialNetwork.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ApplicationDb16 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserTasks", "AttemptsAmount", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.UserTasks", "AttemptsAmount");
        }
    }
}
