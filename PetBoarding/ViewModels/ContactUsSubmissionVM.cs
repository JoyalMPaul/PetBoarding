using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PetBoarding.ViewModels
{
    public class ContactUsSubmissionVM
    {
        [Required(ErrorMessage = "Enter a valid First Name")]
        [StringLength(50, ErrorMessage = "First Name should be between 3 and 50 characters", MinimumLength = 3)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Enter a valid Last Name.")]
        [StringLength(50, ErrorMessage = "Last Name should be between 3 and 50 characters", MinimumLength = 3)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Enter a valid email")]
        [EmailAddress(ErrorMessage = "Enter a valid email address")]
        [StringLength(50, ErrorMessage = "Email should be between 5 and 50 characters", MinimumLength = 5)]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Enter a valid phone number")]
        [StringLength(10, ErrorMessage = "Phone number should be between 10 characters", MinimumLength = 10)]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Enter a valid reason between 3 to 500 characters")]
        [StringLength(500,  MinimumLength = 3)]
        [Display(Name = "Reason For Contact")]
        public string ReasonForContact { get; set; }

        [Required(ErrorMessage = "Enter one word to describe the reason for contact")]
        [StringLength(30, ErrorMessage = "Basic info should be between 3 and 30 characters", MinimumLength = 3)]
        [Display(Name = "Basic Info")]
        public string BasicInfo { get; set; }
    }
}