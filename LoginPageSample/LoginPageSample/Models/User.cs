using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LoginPageSample.Models
{
    public class User
    {
        public int Id { get; set; }
        [Required(ErrorMessage= "Username is required.")]
        [StringLength(20,MinimumLength =3,ErrorMessage="username cant exceed 20 characters.")]
        public string Username { get; set; }
        [Required]
        [StringLength(10,MinimumLength =6,ErrorMessage ="Password must be at least 6 character long.")]
        public string Password { get; set; }
        [Required]
        [EmailAddress(ErrorMessage="Invalid Email Address format")]
        public String Email { get; set; }
    }
}