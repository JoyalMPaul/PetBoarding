using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PetBoarding.ViewModels
{
    public class PetAddVM
    {
        [Required(ErrorMessage = "Enter a valid name")]
        [StringLength(50, ErrorMessage = "Name should be between 3 and 50 characters", MinimumLength = 3)]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required]
        [StringLength(25, ErrorMessage = "Male or Female", MinimumLength = 1)]
        [Display(Name = "Gender")]
        public string Gender { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Type a valid breed for your pet", MinimumLength = 1)]
        [Display(Name = "Breed")]
        public string Breed { get; set; }


        [Required]
        [Range(1, 50, ErrorMessage = "Age must be between {1} and {2}")]
        [Display(Name = "Age")]
        public int Age { get; set; }

        [Required]
        [StringLength(10, ErrorMessage = "The Phone number should be only digits, no special characters", MinimumLength = 10)]
        [Display(Name = "Emergency Contact")]
        public string EmergencyContact { get; set; }

        [StringLength(500, ErrorMessage = "Put N/A if nothing specific to put")]
        [Display(Name = "Dietary Instructions")]
        public string DietaryInstructions { get; set; }
    }
}