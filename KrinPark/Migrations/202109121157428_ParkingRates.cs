namespace KrinPark.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ParkingRates : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ParkingRates",
                c => new
                    {
                        ParkingrateId = c.Guid(nullable: false),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CreatedOn = c.DateTime(nullable: false),
                        UpdatedOn = c.DateTime(nullable: false),
                        CreatedBy = c.String(),
                        UpdatedBy = c.String(),
                    })
                .PrimaryKey(t => t.ParkingrateId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ParkingRates");
        }
    }
}
