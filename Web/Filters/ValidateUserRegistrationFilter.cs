using Core.DTO.Identity;
using Core.ServiceContracts;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Web.Filters;

public class ValidateUserRegistrationFilter : IAsyncActionFilter
{
    private readonly IUserService _userService;

    public ValidateUserRegistrationFilter(IUserService userService)
    {
        _userService = userService;
    }
    
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        if (context.ActionArguments.ContainsKey("userRequest") &&
            context.ActionArguments["userRequest"] is RegisterUserRequest userRequest)
        {
            var existingUserByEmail = await _userService.GetByEmailAsync(userRequest.Email);
            if (existingUserByEmail != null)
            {
                context.ModelState.AddModelError("Email", "Email is already taken.");
            }
            
            var existingUserByUsername = await _userService.GetByUserNameAsync(userRequest.UserName);
            if (existingUserByUsername != null)
            {
                context.ModelState.AddModelError("UserName", "Username is already taken.");
            }
        }

        await next();
    }
}