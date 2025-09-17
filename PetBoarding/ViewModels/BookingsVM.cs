using PetBoarding.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PetBoarding.ViewModels
{
    public class BookingsVM
    {
        [Required(ErrorMessage = "Enter a valid name")]
        [Display(Name = "Pet ID and Name")]
        public Guid PetID { get; set; }

        [Required]
        [Display(Name = "Book In Time")]
        public DateTime BookInTime { get; set; }

        [Required]
        [Display(Name = "Book Out Time")]
        public DateTime BookOutTime { get; set; }

        public IEnumerable<SelectListItem> AvailablePets { get; set; }
    }
}