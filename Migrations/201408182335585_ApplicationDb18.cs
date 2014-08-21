namespace SocialNetwork.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ApplicationDb18 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.UserSolvedTasks", "AttemptAmount");
            DropColumn("dbo.UserSolvedTasks", "IsSolved");
        }
        
        public override void Down()
        {
            AddColumn("dbo.UserSolvedTasks", "IsSolved", c => c.Boolean(nullable: false));
            AddColumn("dbo.UserSolvedTasks", "AttemptAmount", c => c.Int(nullable: false));
        }
    }
}
