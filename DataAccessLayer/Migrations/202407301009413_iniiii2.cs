namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class iniiii2 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Carts", "total");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Carts", "total", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
    }
}
