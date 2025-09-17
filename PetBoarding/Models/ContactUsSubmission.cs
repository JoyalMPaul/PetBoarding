using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PetBoarding.Models
{
    public class ContactUsModel
    {
        [Key]
        public Guid ContactID { get; set; }

        [Required, MaxLength(50)]
        public string FirstName { get; set; }

        [Required, MaxLength(50)]
        public string LastName { get; set; }

        [Required, MaxLength(320)]
        public string Email { get; set; }

        [MaxLength(10)]
        public string PhoneNumber { get; set; }

        [Required, MaxLength(500)]
        public string ReasonForContact { get; set; }

        public string BasicInfo { get; set; }

        public ContactUsModel () {
            ContactID = Guid.NewGuid ();
        }

        public ContactUsModel(string fname, string lname, string email, string phonenumber, string reason, string basicinfo) : this()
        {
            FirstName = fname;
            LastName = lname;
            Email = email;
            PhoneNumber = phonenumber;
            ReasonForContact = reason;
            BasicInfo = basicinfo; 
        }
    }
}