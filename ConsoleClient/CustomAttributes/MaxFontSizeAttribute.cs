using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleClient.CustomAttributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class MaxFontSizeAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
        {
            if (value is CommandLineOptions obj)
            {
                if (obj.MaxFontSize < obj.MinFontSize)
                {
                    return new ValidationResult("MaxFontSize must be greater than or equal to MinFontSize.");
                }
            }
            return ValidationResult.Success;
        }
    }
}
