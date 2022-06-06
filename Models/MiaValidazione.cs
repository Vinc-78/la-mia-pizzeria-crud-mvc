using System.ComponentModel.DataAnnotations;

namespace la_mia_pizzeria_static.Models
{
    public class MiaValidazione
    {
        public class CinqueParole : ValidationAttribute
        {
            protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
            {
                string fieldValue = (string)value;

                string[] vett = fieldValue.Trim().Split(" ");

                if (fieldValue == null || vett.Length <5 )
                {
                    return new ValidationResult("il campo deve contenere almeno cinque parole");
                }

                return ValidationResult.Success;
            }
        }
    }
}
