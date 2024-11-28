using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SAMPLE1.Models
{
    public class Customer
    {
        [Key]
        public int CustomerId { get; set; }

        [Required,StringLength(20,MinimumLength=3)]
        public string CustomerName { get; set; }

        
        [Required,StringLength(20,MinimumLength =3)]
        public string CustomerAddress { get; set; }

        [Required]
        public long CustomerPhoneNo { get; set; }

    }
}
