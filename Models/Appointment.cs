using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StanfordHospital.Models
{
    public class Appointment
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Appointmentid { get; set; }

        [ForeignKey("Patient")]
        public int Patientid { get; set; }
        public virtual Patient? Patient { get; set; }

        [ForeignKey("User")]
        public string? Id { get; set; }
        public virtual User? User { get; set; }

        [Required]
        [Display(Name = "Appointment Date")]
        public DateTime AppointmentDate { get; set; }
        [Required]
        [Display(Name = "Appointment Time")]
        public DateTime AppointmentTime { get; set;}
        [Required]
        [Display(Name = "Appointment Status")]
        public string? AppointmentStatus { get; set;}
        [Required]
        [Display(Name = "ReasonForAppointment")]
        public string? ReasonForAppointment { get; set; }
    }
}
