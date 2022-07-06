using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeServiceTracker.Shared.Models.ScheduledService
{
    public class ScheduledServiceEdit
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public int ServiceItemId { get; set; }
        public int HomeId { get; set; }
        // public DateTime? LastServiceDate { get; set; }
        // public DateTime? NextServiceDate { get; set; }
        public DateTime? ScheduledServiceDate { get; set; }
        public bool ServiceCompleted { get; set; }
        public int ServiceProviderId { get; set; }
        public float ServiceCost { get; set; }
        public int ServiceRating { get; set; }
    }
}
