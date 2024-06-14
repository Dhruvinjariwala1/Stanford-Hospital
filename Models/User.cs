using System.ComponentModel.DataAnnotations;

namespace StanfordHospital.Models
{
    public class User
    {
        public string? Id { get; set; }

        [Required]
        [Display(Name = "First Name")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "First Name can only contain letters.")]
        public string? FirstName { get; set; }
        [Display(Name = "Last Name")]
        [Required]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Last Name can only contain letters.")]
        public string? LastName { get; set; }
        [Required]
        [EmailAddress]
        [RegularExpression(".+\\@.+\\..+", ErrorMessage ="Please Enter Valid Email Address")]
        public string? Email { get; set; }
        [Display(Name = "Phone No")]
        [Required]
        [RegularExpression(@"^[0-9]{10}$", ErrorMessage = "Phone No must be 10 digits.")]
        public string? PhoneNo { get; set; }
        [Display(Name ="Address")]
        [Required]
        public string? Address { get; set; }
        [Display(Name = "Birth Date")]
        [Required]
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }
        [Required]
        public string? Gender { get; set; }
        public string? Image { get; set; }
        public string? Role { get; set; }
        public string? Action { get; set; }
    }
}
