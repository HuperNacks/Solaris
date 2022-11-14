using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace RealEstate.Areas.Admin.Models
{
    public class AddUserViewModel
    {
        public string Id { get; set; }

        [Required]
        [StringLength(30, ErrorMessage = "The first name field should have a maximum of 30 characters")]
        [Display(Name = "Firstname")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(30, ErrorMessage = "The last name field should have a maximum of 30 characters")]
        [Display(Name = "Lastname")]
        public string LastName { get; set; }
        

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

    }

}

