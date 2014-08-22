namespace SocialNetwork.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ApplicationDb19 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ChartModels",
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
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.UserTasks", t => t.UserTask_Id)
                .Index(t => t.UserTask_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ChartModels", "UserTask_Id", "dbo.UserTasks");
            DropIndex("dbo.ChartModels", new[] { "UserTask_Id" });
            DropTable("dbo.ChartModels");
        }
    }
}
