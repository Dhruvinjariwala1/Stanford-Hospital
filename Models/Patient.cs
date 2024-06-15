using System.ComponentModel.DataAnnotations;

namespace StanfordHospital.Models
{
    public class Patient
    {
        public int Patientid { get; set; }

        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "First Name can only contain letters.")]
        [Display(Name ="First Name")]
        public string? Firstname { get; set;}
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Last Name can only contain letters.")]
        [Display(Name ="Last Name")]
        public string? Lastname { get; set;}
        [Required]
        public string? Gender { get; set;}
        [Required]
        [Display(Name ="Date Of Birth")]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }       
        [Display(Name ="Contact Number")]
        [RegularExpression(@"^[0-9]{10}$", ErrorMessage = "Contact Number must be 10 digits.")]
        public string? ContactNumber { get; set; }
        [RegularExpression(".+\\@.+\\..+", ErrorMessage = "Please Enter Valid Email Address")]
        [EmailAddress]
        [Display(Name ="Email Id")]
        public string? EmailId { get; set; }
        [Display(Name ="Age")]
        [Required]
        public string? Age { get; set; }
        [Required]
        [Display(Name ="Address")]
        public string? Address { get; set;}
        public string? Action { get; set; }
    }
}
