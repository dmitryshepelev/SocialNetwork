namespace SocialNetwork.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ApplicationDb22 : DbMigration
    {
        public override void Up()
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
                        UserTaskId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.UserTasks", t => t.UserTaskId, cascadeDelete: true)
                .Index(t => t.UserTaskId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Charts", "UserTaskId", "dbo.UserTasks");
            DropIndex("dbo.Charts", new[] { "UserTaskId" });
            DropTable("dbo.Charts");
        }
    }
}
