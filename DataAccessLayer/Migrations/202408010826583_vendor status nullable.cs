namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class vendorstatusnullable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Users", "VendorStatus", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Users", "VendorStatus", c => c.Int(nullable: false));
        }
    }
}
