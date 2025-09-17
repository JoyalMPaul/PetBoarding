namespace PetBoarding.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateBookingDateTimeProperties : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.BookingsModels", "BookInTime", c => c.DateTime(nullable: false));
            AlterColumn("dbo.BookingsModels", "BookOutTime", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.BookingsModels", "BookOutTime", c => c.String(nullable: false));
            AlterColumn("dbo.BookingsModels", "BookInTime", c => c.String(nullable: false));
        }
    }
}
