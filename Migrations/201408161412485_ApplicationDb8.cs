namespace SocialNetwork.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ApplicationDb8 : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.UserTasks", name: "Category_Id", newName: "CategoryModel_Id");
            RenameIndex(table: "dbo.UserTasks", name: "IX_Category_Id", newName: "IX_CategoryModel_Id");
            AddColumn("dbo.UserTasks", "CategoryId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.UserTasks", "CategoryId");
            RenameIndex(table: "dbo.UserTasks", name: "IX_CategoryModel_Id", newName: "IX_Category_Id");
            RenameColumn(table: "dbo.UserTasks", name: "CategoryModel_Id", newName: "Category_Id");
        }
    }
}
