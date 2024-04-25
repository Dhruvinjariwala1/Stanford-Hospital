using Microsoft.AspNetCore.Identity;
using Microsoft.VisualBasic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StanfordHospital.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        [Display(Name ="FirstName")]
        public string? FirstName { get; set; }
        [Required]
        [Display(Name = "LastName")]
        public string? LastName { get; set; }
        [MaxLength(10)]
        public string? PhoneNo { get; set; }
        public string? Address { get; set; }
        [Required]
        [Display(Name = "DateOfBirth")]
        public DateTime BirthDate { get; set; }
        [Required]
        public string? Gender { get; set; }
        public string? Role { get; set; }
        public string? Image { get; set; }
    }
}
