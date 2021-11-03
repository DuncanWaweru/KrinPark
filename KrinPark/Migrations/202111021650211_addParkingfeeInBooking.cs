namespace KrinPark.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addParkingfeeInBooking : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Bookings", "ParkingFee", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Bookings", "ParkingFee");
        }
    }
}
