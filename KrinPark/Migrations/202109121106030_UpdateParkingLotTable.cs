namespace KrinPark.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateParkingLotTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ParkingLots", "CreatedOn", c => c.DateTime(nullable: false));
            AddColumn("dbo.ParkingLots", "UpdatedOn", c => c.DateTime(nullable: false));
            AddColumn("dbo.ParkingLots", "CreatedBy", c => c.String());
            AddColumn("dbo.ParkingLots", "UpdatedBy", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ParkingLots", "UpdatedBy");
            DropColumn("dbo.ParkingLots", "CreatedBy");
            DropColumn("dbo.ParkingLots", "UpdatedOn");
            DropColumn("dbo.ParkingLots", "CreatedOn");
        }
    }
}
