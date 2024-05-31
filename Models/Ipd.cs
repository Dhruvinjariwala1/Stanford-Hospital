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

        
        [Display(Name = "Admit Date")]
        [DataType(DataType.DateTime)]
        public DateTime AdmitDate { get; set; }
        
        [Display(Name = "Discharge Date")]
        [DataType(DataType.DateTime)]
        public DateTime DischargeDate { get; set; }
        
        public string? Diagnosis { get; set; }

        [Display(Name = "Prescription")]
        public string? Prescription { get; set; }

        [Display(Name = "Diagnosis Charges")]
        public int? DiagnosisCharges { get; set; }

        [Display(Name ="Extra Charges")]
        public string? ExtraCharges { get; set; }

        [Display(Name ="Room Type")]
        public string? RoomType { get; set; }

        [Display(Name = "Room Charges")]
        public int? RoomCharges { get; set; }

        [Display(Name ="Per Day Room ")]
        public int? PerDayRoom { get; set; }

        [Display(Name = "Total Room Price ")]
        public decimal? TotalRoomPrice { get; set; }

        [Display(Name = " Mediclaim Name")]
        public string? MediclaimName { get; set; }

        [Display(Name = "Insurance Number")]
        public string? InsuranceNumber { get; set; }

        [Display(Name = "CashLess")]
        public string? CashLess {  get; set; }
    }
}
