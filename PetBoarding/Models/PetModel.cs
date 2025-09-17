using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PetBoarding.Models
{
    public class PetModel
    {
        [Key]
        public Guid PetID { get; set; }

        public virtual List<BookingsModel> Bookings { get; set; }

        [Required, MaxLength(50)]
        public string Name { get; set; }

        [Required, MaxLength(50)]
        public string Gender { get; set; }

        [Required, MaxLength(50)]
        public string Breed { get; set; }

        [Required, Range(0, 50)]
        public int Age { get; set; }

        [MaxLength(10)]
        public string EmergencyContactNumber { get; set; }

        [MaxLength(500)]
        public string DietaryInstructions { get; set; }
        
        public PetModel() { 
            PetID = Guid.NewGuid();
        }
    }
}