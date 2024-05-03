using System.ComponentModel.DataAnnotations;

namespace StanfordHospital.Models
{
    public class Room
    {
        public int Roomid { get; set; }

        [Required(ErrorMessage ="*")]
        [Display(Name ="RoomName")]
        public string? RoomName { get; set; }

        [Required(ErrorMessage ="*")]
        [Display(Name ="RoomType")]
        public string? RoomType { get; set; }

        [Required(ErrorMessage ="*")]
        [Display(Name ="RoomPrice")]
        public int RoomPrice { get; set; }
    }
}
