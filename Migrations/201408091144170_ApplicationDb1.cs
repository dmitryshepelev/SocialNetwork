namespace SocialNetwork.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ApplicationDb1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "TaskAmount", c => c.Int());
            AddColumn("dbo.AspNetUsers", "SolutionAmount", c => c.Int());
            AddColumn("dbo.AspNetUsers", "Discriminator", c => c.String(nullable: false, maxLength: 128));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "Discriminator");
            DropColumn("dbo.AspNetUsers", "SolutionAmount");
            DropColumn("dbo.AspNetUsers", "TaskAmount");
        }
    }
}
