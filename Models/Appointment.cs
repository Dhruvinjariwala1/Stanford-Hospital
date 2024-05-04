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
        [Required(ErrorMessage = "*")]
        public int Patientid { get; set; }
        public virtual Patient? Patient { get; set; }

        [ForeignKey("User")]
        [Required(ErrorMessage = "*")]
        public string? Id { get; set; }
        public virtual ApplicationUser? User { get; set; }

        [Required(ErrorMessage ="*")]
        [Display(Name = "Appointment Date")]
        [DataType(DataType.Date)]
        public DateTime AppointmentDate { get; set; }
        [Required(ErrorMessage = "*")]
        [Display(Name = "Appointment Time")]
        [DataType(DataType.Time)]
        public DateTime AppointmentTime { get; set;}
        [Required(ErrorMessage ="*")]
        [Display(Name = "Appointment Status")]
        public string? AppointmentStatus { get; set;}
        [Required(ErrorMessage = "*")]
        [Display(Name = "Reason For Appointment")]
        public string? ReasonForAppointment { get; set; }

        
    }
}
