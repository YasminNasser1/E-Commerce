namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class userhave3typesandtheadditionofvendorcertificate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "Role", c => c.Int(nullable: false));
            AddColumn("dbo.Users", "VendorCertificateNumber", c => c.String());
            AddColumn("dbo.Users", "VendorCertificateImage", c => c.Binary());
            AlterColumn("dbo.Users", "LastName", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Users", "LastName", c => c.String());
            DropColumn("dbo.Users", "VendorCertificateImage");
            DropColumn("dbo.Users", "VendorCertificateNumber");
            DropColumn("dbo.Users", "Role");
        }
    }
}
