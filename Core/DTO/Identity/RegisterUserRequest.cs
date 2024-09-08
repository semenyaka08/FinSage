using System.ComponentModel.DataAnnotations;

namespace Core.DTO.Identity;

public class RegisterUserRequest
{
    [Required]
    public string UserName { get; set; } = string.Empty;
    
    [EmailAddress(ErrorMessage = "Invalid email address.")]
    [Required]
    public string Email { get; set; } = string.Empty;
    
    [Required]
    public string Password { get; set; } = string.Empty;
}
