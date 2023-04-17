using System.ComponentModel.DataAnnotations;
namespace MVCAppSystem.Models
{
    public class Room
    {
            [Key]
           [Required]
           public int RoomNo { get; set; }
           [Required]
           public double RoomRent { get; set; }
           [Required]
           public string RoomType { get; set; }
           [Required]
           public int Bed { get; set; }
           [Required]
           public bool IsAllocated { get; set; } = false;   
    }
}
