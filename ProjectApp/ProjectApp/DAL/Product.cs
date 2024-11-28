using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ProjectApp.DAL
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required, StringLength(100, MinimumLength = 3)]
        [Display(Name = "ProductName")]
        public string Name { get; set; }

        [Range(0.01, 1000)]
        public decimal Price { get; set; }

        [Range(0, 100)]
        public int Quantity { get; set; }
    }

    public class ProductDTO
    {   
       
        public string Name { get; set; } 
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }

    }
