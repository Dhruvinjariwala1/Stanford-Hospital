using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace StanfordHospital.Models.Dtos
{
    public class AppointmentDto
    {
        public int Appointmentid { get; set; }

        [ForeignKey("Patient")]
        [Required]
        public int Patientid { get; set; }
        public virtual Patient? Patient { get; set; }

        [ForeignKey("User")]
        [Required]
        public string? Id { get; set; }
        public virtual ApplicationUser? User { get; set; }

        [Required]
        [Display(Name = "Appointment Date")]
        [DataType(DataType.Date)]
        public DateTime AppointmentDate { get; set; }
        [Required]
        [Display(Name = "Appointment Time")]
        [DataType(DataType.Time)]
        public DateTime AppointmentTime { get; set; }
        [Required]
        [Display(Name = "Appointment Status")]
        public string? AppointmentStatus { get; set; }

        public List<string> MultipleDiagnosis { get; set; }

        [Display(Name = "Prescription")]
        public string? Prescription { get; set; }

        [Required]
        [Display(Name = "Reason For Appointment")]
        public string? ReasonForAppointment { get; set; }

        [Required]
        [Display(Name = "Cases")]
        public string? Cases { get; set; }

        [Required]
        [Display(Name = "Price")]
        public int? Price { get; set; }
    }
}
