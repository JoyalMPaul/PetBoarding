using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PetBoarding.Models
{
    public class BookingsModel
    {
        [Key]
        public Guid BookingID { get; set; }

        [Required]
        public virtual PetModel Pet { get; set; }

        [Required]
        public DateTime BookInTime { get; set; }

        [Required]
        public DateTime BookOutTime { get; set; }

        [MaxLength(50)]
        public string RoomNumber { get; set; }

        public int CareTakerID { get; set; }

        public BookingsModel() { 
            BookingID = Guid.NewGuid();
        }
    }
}