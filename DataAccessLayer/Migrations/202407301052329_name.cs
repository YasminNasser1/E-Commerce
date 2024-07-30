namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class name : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Products", name: "ProductTypeyId", newName: "ProductTypeId");
            RenameIndex(table: "dbo.Products", name: "IX_ProductTypeyId", newName: "IX_ProductTypeId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Products", name: "IX_ProductTypeId", newName: "IX_ProductTypeyId");
            RenameColumn(table: "dbo.Products", name: "ProductTypeId", newName: "ProductTypeyId");
        }
    }
}
