using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Zadatak1.Models
{
    public class Stavka
    {
        public int IDStavka { get; set; }
        public int RacunID { get; set; }
        public int Kolicina { get; set; }
        public int ProizvodID { get; set; }
        public decimal CijenaPoKomadu { get; set; }
        public decimal PopustUPostocima { get; set; }
        public decimal UkupnaCijena { get; set; }

        public override string ToString()
        {
            return $"{IDStavka} {RacunID} {Kolicina}" +
                $"{ProizvodID} {CijenaPoKomadu} {PopustUPostocima}" +
                $"{UkupnaCijena}";
        }
    }
}