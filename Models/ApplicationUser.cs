using Microsoft.AspNetCore.Identity;
using Microsoft.VisualBasic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StanfordHospital.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        [Display(Name ="First Name")]
        public string? FirstName { get; set; }
        [Required]
        [Display(Name = "Last Name")]
        public string? LastName { get; set; }
        [MaxLength(10)]
        [Display(Name = "Phone No")]
        public string? PhoneNo { get; set; }
        [Required]
        [Display(Name ="Address")]
        public string? Address { get; set; }
        [Required]
        [Display(Name = "Date Of Birth")]
        public DateTime BirthDate { get; set; }
        [Required]
        public string? Gender { get; set; }
        public string? Role { get; set; }
        public string? Image { get; set; }
    }
}
