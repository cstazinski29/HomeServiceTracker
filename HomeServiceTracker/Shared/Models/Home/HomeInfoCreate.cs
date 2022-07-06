using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeServiceTracker.Shared.Models.Home
{
    public class HomeInfoCreate
    {
        [Required]
        public string HomeName { get; set; }
        public int BuildYear { get; set; }
        public int SquareFootage { get; set; }
        public int Beds { get; set; }
        public float Baths { get; set; }
        // public int PrimaryHomeownerId { get; set; }
        // public DateTimeOffset CreatedUtc { get; set; }
        // public DateTimeOffset? ModifiedUtc { get; set; }
    }
}