using System.ComponentModel.DataAnnotations;

namespace HomeServiceTracker.Server.Models
{
    public class ServiceItem
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string ServiceName { get; set; }
        [Required]
        public string ServiceDescription { get; set; }
        [Required]
        public string ServiceFrequency { get; set; }
        public Guid OwnerId { get; set; }
    }
}
