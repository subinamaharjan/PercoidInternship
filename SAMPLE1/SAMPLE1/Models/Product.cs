using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SAMPLE1.Models
{
    public class Product
    {
        [Key]
        public int Pid { get; set; }

        [Required,StringLength(100,MinimumLength=3)]
        public string PName { get; set; }

        [Range(0.00,10000)]
        public decimal Price { get; set; }

        [Required,Range(0,100)]
        public int Quantity { get; set; }
    }
}
