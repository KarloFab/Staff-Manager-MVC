using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Zadatak1.Models
{
    public class User
    {
        public Person Person { get; set; }

        [Required(ErrorMessage = "Username is required")]
        [MaxLength(10,ErrorMessage ="Max 10 letters")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [MaxLength(10,ErrorMessage = "Max 10 letters")]
        public string Password { get; set; }

        [EmailAddress(ErrorMessage = "Kriva email adresa")]
        public string Email{ get; set; }
    }
}