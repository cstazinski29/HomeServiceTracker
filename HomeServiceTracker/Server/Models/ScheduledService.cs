using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HomeServiceTracker.Server.Models
{
    public class ScheduledService
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [ForeignKey("ServiceItem")]
        public int ServiceItemId { get; set; }
        [Required]
        [ForeignKey("HomeInfo")]
        public int HomeId { get; set; }
        [Required]
        [ForeignKey("ServiceProviderInfo")]
        public int ServiceProviderId { get; set; }
        public DateTime? LastServiceDate { get; set; }
        public DateTime? NextServiceDate { get; set; }
        [Required]
        public DateTime? ScheduledServiceDate { get; set; }
        public bool ServiceCompleted { get; set; }
        public float ServiceCost { get; set; }
        public int ServiceRating { get; set; }
        public Guid OwnerId { get; set; }

        public virtual HomeInfo HomeInfo { get; set; }
        public virtual ServiceItem ServiceItem { get; set; }
        public virtual ServiceProviderInfo ServiceProviderInfo { get; set; }
    }
}
