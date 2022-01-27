namespace AleksandarMihaljev.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class druga : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Buses", "Line", c => c.String(nullable: false, maxLength: 10));
            DropColumn("dbo.Buses", "BusLine");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Buses", "BusLine", c => c.String(nullable: false, maxLength: 10));
            DropColumn("dbo.Buses", "Line");
        }
    }
}
