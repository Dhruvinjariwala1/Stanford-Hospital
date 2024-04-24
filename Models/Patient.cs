using System.ComponentModel.DataAnnotations;

namespace StanfordHospital.Models
{
    public class Patient
    {
        public int Patientid { get; set; }

        [Required]
        [Display(Name ="First Name")]
        public string? Firstname { get; set;}
        [Required]
        [Display(Name ="Last Name")]
        public string? Lastname { get; set;}
        [Required]
        public string? Gender { get; set;}
        [Required]
        [Display(Name ="Date Of Birth")]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }
        [MaxLength(10)]
        [Display(Name ="Contact Number")]
        [Required]
        public string? ContactNumber { get; set; }
        [Required]
        [Display(Name ="Email Id")]
        public string? EmailId { get; set; }
        [Required]
        [MaxLength(3)]
        [Display(Name ="Age")]
        public string? Age { get; set; }
        public string? Address { get; set;}
        //public string? Action { get; set;}
    }
}
