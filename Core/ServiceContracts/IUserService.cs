using Core.Domain.Models;
using Core.DTO.Identity;

namespace Core.ServiceContracts;

public interface IUserService
{
    Task<User?> GetByUserNameAsync(string userName);
    
    Task<User?> GetByEmailAsync(string email);

    Task<User> RegisterUserAsync(RegisterUserRequest userRequest);
    
    Task<(User? user, string? token)> LoginUserAsync(LoginUserRequest userRequest);
}