namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class productupdateinmodel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "QuantitySold", c => c.Int(nullable: false));
            AddColumn("dbo.Products", "TotalSales", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "TotalSales");
            DropColumn("dbo.Products", "QuantitySold");
        }
    }
}
