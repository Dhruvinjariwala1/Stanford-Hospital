using System.ComponentModel.DataAnnotations;

namespace StanfordHospital.Models
{
    public class User
    {
        public string? Id { get; set; }

        [Required(ErrorMessage = "*")]
        [Display(Name = "First Name")]
        public string? FirstName { get; set; }
        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "*")]
        public string? LastName { get; set; }
        [Required(ErrorMessage = "*")]
        [EmailAddress]
        public string? Email { get; set; }
        [Display(Name = "Phone No")]
        [Required(ErrorMessage = "*")]
        [MaxLength(10)]
        public string? PhoneNo { get; set; }
        public string? Address { get; set; }
        [Display(Name = "Birth Date")]
        [Required(ErrorMessage = "*")]
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }
        [Required(ErrorMessage ="*")]
        public string? Gender { get; set; }
        public string? Image { get; set; }
        public string? Role { get; set; }
        public string? Action { get; set; }
    }
}
