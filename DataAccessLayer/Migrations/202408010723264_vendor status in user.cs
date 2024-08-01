namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class vendorstatusinuser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "VendorStatus", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "VendorStatus");
        }
    }
}
