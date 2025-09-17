namespace PetBoarding.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixPetToOwnerLink : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.PetModels", "User_Id", "dbo.AspNetUsers");
            DropIndex("dbo.PetModels", new[] { "User_Id" });
            DropColumn("dbo.PetModels", "User_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PetModels", "User_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.PetModels", "User_Id");
            AddForeignKey("dbo.PetModels", "User_Id", "dbo.AspNetUsers", "Id");
        }
    }
}
