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
        //I think this may want to go back to a string; I set is as a guid to make it equal to the _userId in the HomeInfoService
        public Guid OwnerId { get; set; }
        //public ICollection<ScheduledService>
    }
}
