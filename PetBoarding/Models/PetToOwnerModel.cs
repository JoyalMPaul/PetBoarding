using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PetBoarding.Models
{
    public class PetToOwnerModel
    {
        [Key]
        public Guid PetToOwnerID { get; set; }

        [Required]
        public Guid PetID { get; set; } 

        [ForeignKey("PetID")]
        public virtual PetModel Pet { get; set; }

        [Required]
        public string OwnerId { get; set; } 

        [ForeignKey("OwnerId")]
        public virtual OwnerModel Owner { get; set; }

        public PetToOwnerModel() { 
            PetToOwnerID = Guid.NewGuid();
        }
    }
}