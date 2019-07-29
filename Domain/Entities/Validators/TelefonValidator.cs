using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Zadatak1.Models
{
    public class TelefonValidator : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var vkupac = validationContext.ObjectInstance as Kupac;

            if (vkupac.Telefon.Length == 10)
            {
                return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult($"Telefon mora imati 10 znakova a ne {vkupac.Telefon.Length}");
            }
        }
    }
}