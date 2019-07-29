using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Zadatak1.Models
{
    public class Person
    {

        public int ID { get; set; }
        [Required(ErrorMessage = "Name is required")]
        public string Ime { get; set; }
        [Required(ErrorMessage = "Surname is required")]
        public string Prezime { get; set; }
    }
}