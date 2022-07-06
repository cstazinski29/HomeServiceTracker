using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeServiceTracker.Shared.Models.ServiceProvider
{
    public class ServiceProviderCreate
    {
        [Required]
        public string ServiceProviderName { get; set; }
    }
}
