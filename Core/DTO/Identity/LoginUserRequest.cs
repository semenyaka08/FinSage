using System.ComponentModel.DataAnnotations;

namespace Core.DTO.Identity;

public class LoginUserRequest
{
    [Required]
    public string UserName { get; set; } = string.Empty;

    [Required]
    public string Password { get; set; } = string.Empty;
}