using System.ComponentModel.DataAnnotations;
using Core.ServiceContracts;

namespace Core.Attributes;

public class UniqueUserName : ValidationAttribute
{
    protected override ValidationResult? IsValid(object value, ValidationContext validationContext)
    {
        var userService = (IUserService)validationContext.GetService(typeof(IUserService))!;
        var userName = value as string;

        var user = userService.GetByUserNameAsync(userName).Result;

        if (user != null)
        {
            return new ValidationResult("User Name is already taken.");
        }

        return ValidationResult.Success;
    }
}