using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeServiceTracker.Shared.Models.HomeInfo
{
    public class HomeInfoEdit
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string HomeName { get; set; }
        public int BuildYear { get; set; }
        public int SquareFootage { get; set; }
        public int Beds { get; set; }
        public float Baths { get; set; }
    }
}
