using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace StanfordHospital.Models.Dtos
{
    public class ReportDto
    {
        public int Reportid { get; set; }

        [ForeignKey("Patient")]
        [Required]
        public int Patientid { get; set; }
        public virtual Patient? Patient { get; set; }

        [ForeignKey("User")]
        [Required]
        public string? Id { get; set; }
        public virtual ApplicationUser? User { get; set; }

        [Required]
        [Display(Name = "Admit Date")]
        [DataType(DataType.DateTime)]
        public DateTime AdmitDate { get; set; }
        [Required]
        [Display(Name = "Discharge Date")]
        [DataType(DataType.DateTime)]
        public DateTime DischargeDate { get; set; }

        public List<string>? MultipleDiagnosis { get; set; }

        [Display(Name = "Prescription")]
        public string? Prescription { get; set; }

        [Display(Name = "Diagnosis Charges")]
        public int? DiagnosisCharges { get; set; }

        public List<string>? MultipleExtraCharges { get; set; }

        [Display(Name = "Room Type")]
        public string? RoomType { get; set; }

        [Display(Name = "Room Charges")]
        public int? RoomCharges { get; set; }

        [Display(Name = "Per Day Room ")]
        public int? PerDayRoom { get; set; }

        [Display(Name = "Total Room Price ")]
        public decimal? TotalRoomPrice { get; set; }

        [Display(Name = " Mediclaim Name")]
        public string? MediclaimName { get; set; }

        [Display(Name = "Insurance Number")]
        public string? InsuranceNumber { get; set; }
    }
}
