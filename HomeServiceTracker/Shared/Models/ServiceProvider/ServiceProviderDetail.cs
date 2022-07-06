using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeServiceTracker.Shared.Models.ServiceProvider
{
    public class ServiceProviderDetail
    {
        public int Id { get; set; }
        public string ServiceProviderName { get; set; }
        public int NumberOfServices { get; set; }
        public float AverageServiceProviderRating { get; set; }
    }
}
