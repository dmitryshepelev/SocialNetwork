namespace SocialNetwork.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ApplicationDb20 : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.ChartModels", newName: "Charts");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.Charts", newName: "ChartModels");
        }
    }
}
