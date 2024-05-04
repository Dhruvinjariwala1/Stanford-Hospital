using System.ComponentModel.DataAnnotations;

namespace StanfordHospital.Models
{
    public class Room
    {
        public int Roomid { get; set; }

        [Required]
        [Display(Name ="Room Name")]
        public string? RoomName { get; set; }

        [Required]
        [Display(Name ="Room Floor")]
        public int RoomFloor { get; set; }

        [Required]
        [Display(Name ="Room Type")]
        public string? RoomType { get; set; }

        [Required]
        [Display(Name ="Room Price")]
        public int RoomPrice { get; set; }
    }
}
