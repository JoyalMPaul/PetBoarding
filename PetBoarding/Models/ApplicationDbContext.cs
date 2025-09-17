using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace PetBoarding.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<PetModel> PetModels { get; set; }
        public DbSet<BookingsModel> BookingsModels { get; set; }
        public DbSet<ContactUsModel> ContactUsModels { get; set; }
        public DbSet<PetToOwnerModel> PetToOwner { get; set; }

        public ApplicationDbContext() : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
    // For every new table, create a public DbSet. then do "add-migration 'Add(TableName)'" and when new file pops up do "update-database"
    // Check any migrations in the Migrations folder if anything needs to be changed
    // To remove, just delete the file and "add-migration 'Remove(TableName)'" and "update-database"
}