namespace PetBoarding.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ResyncDatabase : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BookingsModels",
                c => new
                    {
                        BookingID = c.Guid(nullable: false),
                        BookInTime = c.String(nullable: false),
                        BookOutTime = c.String(nullable: false),
                        RoomNumber = c.String(maxLength: 50),
                        CareTakerID = c.Int(nullable: false),
                        Pet_PetID = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.BookingID)
                .ForeignKey("dbo.PetModels", t => t.Pet_PetID, cascadeDelete: true)
                .Index(t => t.Pet_PetID);
            
            CreateTable(
                "dbo.PetModels",
                c => new
                    {
                        PetID = c.Guid(nullable: false),
                        Name = c.String(nullable: false, maxLength: 50),
                        Gender = c.String(maxLength: 50),
                        Breed = c.String(nullable: false, maxLength: 50),
                        Age = c.Int(nullable: false),
                        EmergencyContactNumber = c.String(maxLength: 10),
                        DietaryInstructions = c.String(maxLength: 500),
                    })
                .PrimaryKey(t => t.PetID);
            
            CreateTable(
                "dbo.ContactUsModels",
                c => new
                    {
                        ContactID = c.Guid(nullable: false),
                        FirstName = c.String(nullable: false, maxLength: 50),
                        LastName = c.String(nullable: false, maxLength: 50),
                        Email = c.String(nullable: false, maxLength: 320),
                        PhoneNumber = c.String(maxLength: 10),
                        ReasonForContact = c.String(nullable: false, maxLength: 500),
                        BasicInfo = c.String(),
                    })
                .PrimaryKey(t => t.ContactID);
            
            CreateTable(
                "dbo.PetToOwnerModels",
                c => new
                    {
                        PetToOwnerID = c.Guid(nullable: false),
                        Owner_Id = c.String(nullable: false, maxLength: 128),
                        Pet_PetID = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.PetToOwnerID)
                .ForeignKey("dbo.AspNetUsers", t => t.Owner_Id, cascadeDelete: true)
                .ForeignKey("dbo.PetModels", t => t.Pet_PetID, cascadeDelete: true)
                .Index(t => t.Owner_Id)
                .Index(t => t.Pet_PetID);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                        FirstName = c.String(maxLength: 50),
                        LastName = c.String(maxLength: 50),
                        Age = c.Int(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.PetToOwnerModels", "Pet_PetID", "dbo.PetModels");
            DropForeignKey("dbo.PetToOwnerModels", "Owner_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.BookingsModels", "Pet_PetID", "dbo.PetModels");
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.PetToOwnerModels", new[] { "Pet_PetID" });
            DropIndex("dbo.PetToOwnerModels", new[] { "Owner_Id" });
            DropIndex("dbo.BookingsModels", new[] { "Pet_PetID" });
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.PetToOwnerModels");
            DropTable("dbo.ContactUsModels");
            DropTable("dbo.PetModels");
            DropTable("dbo.BookingsModels");
        }
    }
}
