using System.ComponentModel.DataAnnotations;

namespace HomeServiceTracker.Server.Models
{
    public class HomeInfo
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string HomeName { get; set; }
        public int BuildYear { get; set; }
        public int SquareFootage { get; set; }
        public int Beds { get; set; }
        public float Baths { get; set; }
        public int PrimaryHomeownerId { get; set; }
        //public ICollection<ScheduledService>
    }
}
