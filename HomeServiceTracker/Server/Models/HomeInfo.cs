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
        public Guid OwnerId { get; set; }
        //public DateTimeOffset CreatedUtc { get; set; }
        //public DateTimeOffset? ModifiedUtc { get; set; }
    }
}
