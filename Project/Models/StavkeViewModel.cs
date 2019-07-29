using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Zadatak1.Models;

namespace Project.Models
{
    public class StavkeViewModel
    {
        public IEnumerable<Stavka> Stavke { get; set; }
        public IEnumerable<Proizvod> Proizvodi { get; set; }
        public bool AllKupci { get; internal set; }
    }
}