namespace PetBoarding.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatePetModelProperties : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.PetModels", "Gender", c => c.String(nullable: false, maxLength: 50));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.PetModels", "Gender", c => c.String(maxLength: 50));
        }
    }
}
