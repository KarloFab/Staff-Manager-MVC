using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Zadatak1.Models;

namespace Project.Models
{
    public class RacunViewModel
    {
        public IEnumerable<Racun> Racuni { get; set; }
        public IEnumerable<Komercijalist> Komercijalist { get; set; }
        public IEnumerable<KreditnaKartica> KreditneKartice { get; set; }
    }
}