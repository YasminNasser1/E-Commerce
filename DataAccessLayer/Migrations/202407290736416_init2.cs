namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Orders", "UserId_Id", "dbo.Users");
            DropIndex("dbo.Orders", new[] { "UserId_Id" });
            RenameColumn(table: "dbo.Orders", name: "UserId_Id", newName: "UserId");
            AddColumn("dbo.Products", "Quantity", c => c.Int(nullable: false));
            AlterColumn("dbo.Orders", "UserId", c => c.Long(nullable: false));
            CreateIndex("dbo.Orders", "UserId");
            AddForeignKey("dbo.Orders", "UserId", "dbo.Users", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Orders", "UserId", "dbo.Users");
            DropIndex("dbo.Orders", new[] { "UserId" });
            AlterColumn("dbo.Orders", "UserId", c => c.Long());
            DropColumn("dbo.Products", "Quantity");
            RenameColumn(table: "dbo.Orders", name: "UserId", newName: "UserId_Id");
            CreateIndex("dbo.Orders", "UserId_Id");
            AddForeignKey("dbo.Orders", "UserId_Id", "dbo.Users", "Id");
        }
    }
}
