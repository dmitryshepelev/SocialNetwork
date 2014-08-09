namespace SocialNetwork.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ApplicationDb2 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.AspNetUsers", "TaskAmount");
            DropColumn("dbo.AspNetUsers", "SolutionAmount");
            DropColumn("dbo.AspNetUsers", "Discriminator");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "Discriminator", c => c.String(nullable: false, maxLength: 128));
            AddColumn("dbo.AspNetUsers", "SolutionAmount", c => c.Int());
            AddColumn("dbo.AspNetUsers", "TaskAmount", c => c.Int());
        }
    }
}
