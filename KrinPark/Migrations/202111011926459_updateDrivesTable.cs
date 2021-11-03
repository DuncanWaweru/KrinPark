namespace KrinPark.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateDrivesTable : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Drivers", name: "ApplicationUser_Id", newName: "ApplicationUserId");
            RenameIndex(table: "dbo.Drivers", name: "IX_ApplicationUser_Id", newName: "IX_ApplicationUserId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Drivers", name: "IX_ApplicationUserId", newName: "IX_ApplicationUser_Id");
            RenameColumn(table: "dbo.Drivers", name: "ApplicationUserId", newName: "ApplicationUser_Id");
        }
    }
}
