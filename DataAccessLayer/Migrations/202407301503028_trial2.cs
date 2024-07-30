namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class trial2 : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.OrderProducts", newName: "ProductOrders");
            DropPrimaryKey("dbo.ProductOrders");
            AddPrimaryKey("dbo.ProductOrders", new[] { "Product_Id", "Order_OrderId" });
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.ProductOrders");
            AddPrimaryKey("dbo.ProductOrders", new[] { "Order_OrderId", "Product_Id" });
            RenameTable(name: "dbo.ProductOrders", newName: "OrderProducts");
        }
    }
}
