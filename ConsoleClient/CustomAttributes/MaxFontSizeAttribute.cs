﻿using System.ComponentModel.DataAnnotations;

namespace ConsoleClient.CustomAttributes;

[AttributeUsage(AttributeTargets.Class)]
public class MaxFontSizeAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
    {
        if (value is Program obj)
            if (obj.MaxFontSize < obj.MinFontSize)
                return new ValidationResult("MaxFontSize must be greater than or equal to MinFontSize.");
        return ValidationResult.Success;
    }
}