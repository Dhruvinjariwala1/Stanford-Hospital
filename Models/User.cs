using System.ComponentModel.DataAnnotations;

namespace StanfordHospital.Models
{
    public class User
    {
        public string? Id { get; set; }

        [Required]
        [Display(Name = "FirstName")]
        public string? FirstName { get; set; }
        [Display(Name = "LastName")]
        [Required]
        public string? LastName { get; set; }
        [Required]
        [EmailAddress]
        public string? Email { get; set; }
        [Display(Name = "PhoneNo")]
        [Required]
        [MaxLength(10)]
        public string? PhoneNo { get; set; }
        public string? Address { get; set; }
        [Display(Name = "BirthDate")]
        [Required]
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }
        [Required]
        public string? Gender { get; set; }
        public string? Image { get; set; }
        public string? Action { get; set; }
    }
}
