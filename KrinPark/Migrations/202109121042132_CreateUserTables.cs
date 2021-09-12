namespace KrinPark.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateUserTables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Bookings",
                c => new
                    {
                        BookingId = c.Guid(nullable: false),
                        VehicleId = c.Guid(nullable: false),
                        CheckIn = c.DateTime(nullable: false),
                        CheckOut = c.DateTime(nullable: false),
                        IsCancelled = c.Boolean(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                        UpdatedOn = c.DateTime(nullable: false),
                        CreatedBy = c.String(),
                        UpdatedBy = c.String(),
                    })
                .PrimaryKey(t => t.BookingId)
                .ForeignKey("dbo.Vehicles", t => t.VehicleId, cascadeDelete: true)
                .Index(t => t.VehicleId);
            
            CreateTable(
                "dbo.Vehicles",
                c => new
                    {
                        VehicleId = c.Guid(nullable: false),
                        DriverId = c.Guid(nullable: false),
                        RegNo = c.String(),
                        CreatedOn = c.DateTime(nullable: false),
                        UpdatedOn = c.DateTime(nullable: false),
                        CreatedBy = c.String(),
                        UpdatedBy = c.String(),
                    })
                .PrimaryKey(t => t.VehicleId)
                .ForeignKey("dbo.Drivers", t => t.DriverId, cascadeDelete: true)
                .Index(t => t.DriverId);
            
            CreateTable(
                "dbo.Drivers",
                c => new
                    {
                        DriverId = c.Guid(nullable: false),
                        Name = c.String(),
                        IdNo = c.String(),
                        Telephone = c.String(),
                        Email = c.String(),
                        Password = c.String(),
                        CreatedOn = c.DateTime(nullable: false),
                        UpdatedOn = c.DateTime(nullable: false),
                        CreatedBy = c.String(),
                        UpdatedBy = c.String(),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.DriverId)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id)
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "dbo.ParkingLots",
                c => new
                    {
                        ParkingLotId = c.Guid(nullable: false),
                        ParkingLotSerialNo = c.String(),
                        ParkingLotStatus = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ParkingLotId);
            
            CreateTable(
                "dbo.Payments",
                c => new
                    {
                        PaymentId = c.Guid(nullable: false),
                        BookingId = c.Guid(nullable: false),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CreatedOn = c.DateTime(nullable: false),
                        UpdatedOn = c.DateTime(nullable: false),
                        CreatedBy = c.String(),
                        UpdatedBy = c.String(),
                    })
                .PrimaryKey(t => t.PaymentId)
                .ForeignKey("dbo.Bookings", t => t.BookingId, cascadeDelete: true)
                .Index(t => t.BookingId);
            
            DropTable("dbo.Parkings");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Parkings",
                c => new
                    {
                        ParkingNo = c.Int(nullable: false, identity: true),
                        ParkingClass = c.String(),
                    })
                .PrimaryKey(t => t.ParkingNo);
            
            DropForeignKey("dbo.Payments", "BookingId", "dbo.Bookings");
            DropForeignKey("dbo.Bookings", "VehicleId", "dbo.Vehicles");
            DropForeignKey("dbo.Vehicles", "DriverId", "dbo.Drivers");
            DropForeignKey("dbo.Drivers", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Payments", new[] { "BookingId" });
            DropIndex("dbo.Drivers", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.Vehicles", new[] { "DriverId" });
            DropIndex("dbo.Bookings", new[] { "VehicleId" });
            DropTable("dbo.Payments");
            DropTable("dbo.ParkingLots");
            DropTable("dbo.Drivers");
            DropTable("dbo.Vehicles");
            DropTable("dbo.Bookings");
        }
    }
}
