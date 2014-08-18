namespace SocialNetwork.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ApplicationDb14 : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.UsrTaskTags", newName: "UserTaskTags");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.UserTaskTags", newName: "UsrTaskTags");
        }
    }
}
