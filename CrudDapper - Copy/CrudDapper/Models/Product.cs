using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace CrudDapper.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [Required,StringLength(20,MinimumLength =3)]
        public string Name { get; set; }
        [Required,Range(0.1,10000)]
        public decimal Price { get; set; }
        [Required,Range(1,100)]
        public int Quantity { get; set; }
        [Required]
        public string Colour { get; set; }

    }
}
