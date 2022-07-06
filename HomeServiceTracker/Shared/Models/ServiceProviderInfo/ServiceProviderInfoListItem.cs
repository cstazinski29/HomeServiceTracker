using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeServiceTracker.Shared.Models.ServiceProviderInfo
{
    public class ServiceProviderInfoListItem
    {
        public int Id { get; set; }
        public string ServiceProviderName { get; set; }
        public float AverageServiceProviderRating { get; set; }
    }
}
