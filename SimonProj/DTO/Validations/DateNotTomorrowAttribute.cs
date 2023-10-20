using System.ComponentModel.DataAnnotations;

namespace SimonProj.DTO.Validations;

public class DateNotTomorrowAttribute : ValidationAttribute
{
    public override bool IsValid(object? value)
    {
        if (value == null)
        {
            return true;
        }

        DateTime dt = (DateTime)value;
        if (dt.Date >= DateTime.Now.AddDays(1).Date)
        {
            return false;
        }

        return true;
    }
}