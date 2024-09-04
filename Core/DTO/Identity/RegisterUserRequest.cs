using System.ComponentModel.DataAnnotations;
using Core.Attributes;

namespace Core.DTO.Identity;

public class RegisterUserRequest
{

    [UniqueUserName]
    [Required]
    public string UserName { get; set; } = string.Empty;
    
    [UniqueEmail]
    [EmailAddress(ErrorMessage = "Invalid email address.")]
    [Required]
    public string Email { get; set; } = string.Empty;
    
    [Required]
    public string Password { get; set; } = string.Empty;
}
