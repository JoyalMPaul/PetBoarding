namespace PetBoarding.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixPetToOwnerRelationships : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.PetToOwnerModels", name: "Owner_Id", newName: "OwnerId");
            RenameColumn(table: "dbo.PetToOwnerModels", name: "Pet_PetID", newName: "PetID");
            RenameIndex(table: "dbo.PetToOwnerModels", name: "IX_Pet_PetID", newName: "IX_PetID");
            RenameIndex(table: "dbo.PetToOwnerModels", name: "IX_Owner_Id", newName: "IX_OwnerId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.PetToOwnerModels", name: "IX_OwnerId", newName: "IX_Owner_Id");
            RenameIndex(table: "dbo.PetToOwnerModels", name: "IX_PetID", newName: "IX_Pet_PetID");
            RenameColumn(table: "dbo.PetToOwnerModels", name: "PetID", newName: "Pet_PetID");
            RenameColumn(table: "dbo.PetToOwnerModels", name: "OwnerId", newName: "Owner_Id");
        }
    }
}
