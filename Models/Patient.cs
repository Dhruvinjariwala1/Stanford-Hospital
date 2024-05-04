using System.ComponentModel.DataAnnotations;

namespace StanfordHospital.Models
{
    public class Patient
    {
        public int Patientid { get; set; }

        [Required(ErrorMessage = "*")]
        [Display(Name ="First Name")]
        public string? Firstname { get; set;}
        [Required(ErrorMessage = "*")]
        [Display(Name ="Last Name")]
        public string? Lastname { get; set;}
        [Required(ErrorMessage = "*")]
        public string? Gender { get; set;}
        [Required(ErrorMessage = "*")]
        [Display(Name ="Date Of Birth")]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }
        [MaxLength(10)]
        [Display(Name ="Contact Number")]
        [Required(ErrorMessage = "*")]
        public string? ContactNumber { get; set; }
        [Required(ErrorMessage ="*")]
        [EmailAddress]
        [Display(Name ="Email Id")]
        public string? EmailId { get; set; }
        [Required(ErrorMessage = "*")]
        [MaxLength(3)]
        [Display(Name ="Age")]
        public string? Age { get; set; }
        public string? Address { get; set;}
        public string? Action { get; set; }

    }
}
