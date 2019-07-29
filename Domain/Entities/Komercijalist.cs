using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Zadatak1.Models
{
    public class Komercijalist 
    {

        public Person Person { get; set; }
        public bool StalniZaposlenik { get; set; }

        public override string ToString()
        {
            return $"{Person.Ime} {Person.Prezime}";
        }
    }
}