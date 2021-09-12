namespace KrinPark.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class improvedtable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Bookings", "ParkingLotId", c => c.Guid(nullable: false));
            CreateIndex("dbo.Bookings", "ParkingLotId");
            AddForeignKey("dbo.Bookings", "ParkingLotId", "dbo.ParkingLots", "ParkingLotId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Bookings", "ParkingLotId", "dbo.ParkingLots");
            DropIndex("dbo.Bookings", new[] { "ParkingLotId" });
            DropColumn("dbo.Bookings", "ParkingLotId");
        }
    }
}
