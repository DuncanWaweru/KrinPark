namespace KrinPark.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class parkingmigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Parkings",
                c => new
                    {
                        ParkingNo = c.Int(nullable: false, identity: true),
                        ParkingClass = c.String(),
                    })
                .PrimaryKey(t => t.ParkingNo);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Parkings");
        }
    }
}
