using Core.DTO.Identity;
using Core.Mapper;
using Core.ServiceContracts;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AccountController : ControllerBase
{
    private readonly IUserService _userService;

    public AccountController(IUserService userService)
    {
        _userService = userService;
    }
    
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterUserRequest userRequest)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await _userService.RegisterUserAsync(userRequest);

        return Ok(result.ToUserRegisterResponse());
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginUserRequest userRequest)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await _userService.LoginUserAsync(userRequest);

        if (result.user == null || result.token == null)
            return BadRequest("User credentials are not correct");
        
        return Ok(new LoginUserResponse(result.token, result.user.UserName, result.user.Email));
    }
}