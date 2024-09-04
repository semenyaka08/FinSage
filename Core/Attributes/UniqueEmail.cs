using System.ComponentModel.DataAnnotations;
using Core.ServiceContracts;

namespace Core.Attributes;

public class UniqueEmail : ValidationAttribute
{
    protected override ValidationResult? IsValid(object value, ValidationContext validationContext)
    {
        var userService = (IUserService)validationContext.GetService(typeof(IUserService))!;
        var email = value as string;

        var user = userService.GetByEmailAsync(email).Result;

        if (user != null)
        {
            return new ValidationResult("Email is already taken.");
        }

        return ValidationResult.Success;
    }
}