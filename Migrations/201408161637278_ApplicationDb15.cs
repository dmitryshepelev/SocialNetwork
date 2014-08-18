namespace SocialNetwork.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ApplicationDb15 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Tags", "TagFrequency");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Tags", "TagFrequency", c => c.Int(nullable: false));
        }
    }
}
