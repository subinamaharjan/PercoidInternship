using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace SuperHeroAPI_DotNet.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required, StringLength(100, MinimumLength=3)]
        public string Name { get; set; }
        
        [Required, Range(0.01,1000)]
        public decimal Price { get; set; }
        
        [Required, Range(0,1000)]
      
        public int Quantity { get; set; }


    }
}
