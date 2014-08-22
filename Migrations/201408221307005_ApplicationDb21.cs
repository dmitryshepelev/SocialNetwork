namespace SocialNetwork.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ApplicationDb21 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Charts", "UserTask_Id", "dbo.UserTasks");
            DropIndex("dbo.Charts", new[] { "UserTask_Id" });
            DropTable("dbo.Charts");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Charts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Expression = c.String(),
                        From = c.String(),
                        To = c.String(),
                        Step = c.String(),
                        AxisXName = c.String(),
                        AxisYName = c.String(),
                        ChartName = c.String(),
                        TaskId = c.Int(nullable: false),
                        UserTask_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateIndex("dbo.Charts", "UserTask_Id");
            AddForeignKey("dbo.Charts", "UserTask_Id", "dbo.UserTasks", "Id");
        }
    }
}
