using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Zadatak1.Models
{
    public class KreditnaKartica
    {
        public int ID { get; set; }
        public string Tip { get; set; }
        public string Broj { get; set; }
        public int IstekMjesec { get; set; }
        public int IstekGodina { get; set; }

        public override string ToString()
        {
            return $"{Tip}";
        }
    }
}