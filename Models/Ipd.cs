using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace StanfordHospital.Models
{
    public class Ipd
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Ipdid { get; set; }

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

        public string? Diagnosis { get; set; }

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


        [Display(Name = "Diagnosis Charges")]
        public int? DiagnosisCharges { get; set; }

        [Display(Name ="Extra Charges")]
        public string? ExtraCharges { get; set; }

        [Display(Name ="Room Type")]
        public string? RoomType { get; set; }

        [Display(Name = "Room Charges")]
        public int? RoomCharges { get; set; }
    }
}
