namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CartUserRelationship : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "FirstName", c => c.String(nullable: false, maxLength: 50));
            AddColumn("dbo.Users", "LastName", c => c.String());
            DropColumn("dbo.Users", "Fname");
            DropColumn("dbo.Users", "Lname");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "Lname", c => c.String());
            AddColumn("dbo.Users", "Fname", c => c.String(nullable: false, maxLength: 50));
            DropColumn("dbo.Users", "LastName");
            DropColumn("dbo.Users", "FirstName");
        }
    }
}
