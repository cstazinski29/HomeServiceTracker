using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeServiceTracker.Shared.Models.ScheduledService
{
    public class ScheduledServiceDetail
    {
        public int Id { get; set; }
        public int ServiceItemId { get; set; }
        public int HomeId { get; set; }
        public DateTime? LastServiceDate { get; set; }
        public DateTime? NextServiceDate { get; set; }
        public DateTime? ScheduledServiceDate { get; set; }
        public bool ServiceCompleted { get; set; }
        public int ServiceProviderId { get; set; }
        public float ServiceCost { get; set; }
        public int ServiceRating { get; set; }
        public string ServiceName { get; set; }
        public string HomeName { get; set; }
        public string ServiceProviderName { get; set; }
    }
}
