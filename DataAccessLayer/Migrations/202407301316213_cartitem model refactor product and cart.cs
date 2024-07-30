namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class cartitemmodelrefactorproductandcart : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ProductCarts", "Product_Id", "dbo.Products");
            DropForeignKey("dbo.ProductCarts", "Cart_Id", "dbo.Carts");
            DropIndex("dbo.ProductCarts", new[] { "Product_Id" });
            DropIndex("dbo.ProductCarts", new[] { "Cart_Id" });
            CreateTable(
                "dbo.CartItems",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CartId = c.Int(nullable: false),
                        ProductId = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Carts", t => t.CartId, cascadeDelete: true)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.CartId)
                .Index(t => t.ProductId);
            
            DropColumn("dbo.Products", "Quantity");
            DropTable("dbo.ProductCarts");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.ProductCarts",
                c => new
                    {
                        Product_Id = c.Int(nullable: false),
                        Cart_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Product_Id, t.Cart_Id });
            
            AddColumn("dbo.Products", "Quantity", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            DropForeignKey("dbo.CartItems", "ProductId", "dbo.Products");
            DropForeignKey("dbo.CartItems", "CartId", "dbo.Carts");
            DropIndex("dbo.CartItems", new[] { "ProductId" });
            DropIndex("dbo.CartItems", new[] { "CartId" });
            DropTable("dbo.CartItems");
            CreateIndex("dbo.ProductCarts", "Cart_Id");
            CreateIndex("dbo.ProductCarts", "Product_Id");
            AddForeignKey("dbo.ProductCarts", "Cart_Id", "dbo.Carts", "Id", cascadeDelete: true);
            AddForeignKey("dbo.ProductCarts", "Product_Id", "dbo.Products", "Id", cascadeDelete: true);
        }
    }
}
