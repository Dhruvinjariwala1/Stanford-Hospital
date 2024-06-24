using Microsoft.AspNetCore.Identity;
using Microsoft.VisualBasic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StanfordHospital.Models
{
    public class ApplicationUser : IdentityUser
    {
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "First Name can only contain letters.")]
        [Display(Name ="First Name")]
        public string? FirstName { get; set; }
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Last Name can only contain letters.")]
        [Display(Name = "Last Name")]
        public string? LastName { get; set; }
        [RegularExpression(@"^[0-9]{10}$", ErrorMessage = "Phone No must be 10 digits.")]
        [Display(Name = "Phone No")]
        public string? PhoneNo { get; set; }
        
        [Display(Name ="Address")]
        public string? Address { get; set; }
        
        [Display(Name = "Date Of Birth")]
        public DateTime BirthDate { get; set; }
        
        public string? Gender { get; set; }
        public string? Role { get; set; }
        public string? Image { get; set; }
    }
}
