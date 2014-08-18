namespace SocialNetwork.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ApplicationDb18 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserSolvedTasks", "AttemptAmount", c => c.Int(nullable: false));
            AddColumn("dbo.UserSolvedTasks", "IsSolved", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.UserSolvedTasks", "IsSolved");
            DropColumn("dbo.UserSolvedTasks", "AttemptAmount");
        }
    }
}
