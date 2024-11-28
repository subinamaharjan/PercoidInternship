using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectProduct.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required,StringLength(20,MinimumLength =3)]
        public string Name { get; set; }

        [Required,Range(0.00,1000)]
        public decimal Price { get; set; }

        [Required,Range(0,100)]
        public int Quantity { get; set; }
    }
}
 