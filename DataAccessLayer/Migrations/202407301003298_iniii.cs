namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class iniii : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Carts", "total", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Products", "Quantity", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Products", "Quantity", c => c.Int());
            DropColumn("dbo.Carts", "total");
        }
    }
}
