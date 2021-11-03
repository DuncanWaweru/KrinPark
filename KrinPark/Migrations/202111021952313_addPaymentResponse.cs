namespace KrinPark.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addPaymentResponse : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PaymentResponses",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Amount = c.Int(nullable: false),
                        Paybill = c.String(nullable: false),
                        AccountNo = c.String(nullable: false),
                        PhoneNumber = c.String(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                        Redeemed = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.PaymentResponses");
        }
    }
}
