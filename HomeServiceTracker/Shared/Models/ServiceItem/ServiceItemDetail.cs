using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeServiceTracker.Shared.Models.ServiceItem
{
    public class ServiceItemDetail
    {
        public int Id { get; set; }
        public string ServiceName { get; set; }
        public string ServiceDescription { get; set; }
        public string ServiceFrequency { get; set; }
    }
}
