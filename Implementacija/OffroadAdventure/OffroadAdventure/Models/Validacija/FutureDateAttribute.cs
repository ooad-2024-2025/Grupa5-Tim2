using System;
using System.ComponentModel.DataAnnotations;

public class FutureDateAttribute : ValidationAttribute
{
    public override bool IsValid(object value)
    {
        if (value == null) return true; 

        DateTime datum;
        if (DateTime.TryParse(value.ToString(), out datum))
        {
            return datum >= DateTime.Today;
        }
        return false;
    }

    public override string FormatErrorMessage(string name)
    {
        return $"{name} mora biti današnji ili budući datum.";
    }
}
