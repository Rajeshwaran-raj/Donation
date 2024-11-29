using System.ComponentModel.DataAnnotations;

namespace Donation.Models
{
    public class Registration
    {

        public int id { get; set; }


        [Display(Name = "Firstname")]
        public string firstname { get; set; }


        [Display(Name = "Lastname")]
        public string lastname { get; set; }



        [Display(Name = "Dateofbirth")]
        [DataType(DataType.Date)]
        public string dateofbirth { get; set; }


        [Display(Name = "Age")]
        public string age { get; set; }



        [Display(Name = "Email")]
        public string Email { get; set; }


        [Display(Name = "Phone")]
        public string phone { get; set; }


        [Display(Name = "Address")]
        public string address { get; set; }


        [Display(Name = "Pincode")]
        public string pincode { get; set; }

        [Required(ErrorMessage = "Enter password")]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        public string password { get; set; }

        [Required(ErrorMessage = "Enter confirm password")]
        [Display(Name = "Confirm password")]
        [DataType(DataType.Password)]
        public string confirmpassword { get; set; }

    }
}