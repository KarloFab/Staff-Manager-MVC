using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Zadatak1.Models;

public class Kupac
{

    public Person Person { get; set; }

    [EmailAddress(ErrorMessage ="Kriva email adresa")]
    public string Email { get; set; }
    [TelefonValidator]
    public string Telefon { get; set; }
    [Display(Name ="Naziv grada")]
    public int GradID { get; set; }
    public int DrzavaID { get; set; }
    public override string ToString()
    {
        return $"{base.ToString()} {Email} {Telefon}";
    }
}
