using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SAMPLE1.Models
{
    public class Employee
    {
        [Key]
        public int Id { get; set; }

        [Required,StringLength(20,MinimumLength=3)]
        public string Name { get; set; }

        [Required,StringLength(20,MinimumLength =3)]
        public string Address { get; set; } 

        [Required]
        public string Post { get; set; }
    }
}
