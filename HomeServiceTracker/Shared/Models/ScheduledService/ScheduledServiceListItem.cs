using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeServiceTracker.Shared.Models.ScheduledService
{
    public class ScheduledServiceListItem
    {
        public int Id { get; set; }
        public int ServiceItemId { get; set; }
        // public int HomeId { get; set; }
        public DateTime? LastServiceDate { get; set; }
        public DateTime? NextServiceDate { get; set; }
        public DateTime? ScheduledServiceDate { get; set; }
        // public bool ServiceCompleted { get; set; }
    }
}
