using System.ComponentModel.DataAnnotations;

namespace MVCAppSystem.Models
{
    public class CustomerHistory
    {   
        public int RoomNo { get; set; }
        [Required]
        public string RoomType { get; set; }
        [Required]
        public int Bed { get; set; }
        [Required]
        public int IdProof { get; set; }
        [Required]
        [Key]
        public string CustomerName { get; set; }
        [Required]
        [EmailAddress]
        public string EmaiId { get; set; }
        [Required]
        public string PhoneNo { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string State { get; set; }
        [Required]
        public int PinCode { get; set; }
        [Required]
        public string Nationality { get; set; }
        [Required]
        public DateTime CheckinTime { get; set; }
        [Required]
        public DateTime Checkout { get; set; }
        public double RoomRent { get; set; }
    }
}
