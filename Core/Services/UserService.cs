using Core.Abstraction;
using Core.Domain.Models;
using Core.Domain.RepositoryContracts;
using Core.DTO.Identity;
using Core.Mapper;
using Core.ServiceContracts;

namespace Core.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IJwtProvider _jwtProvider;

    public UserService(IUserRepository userRepository, IPasswordHasher passwordHasher, IJwtProvider jwtProvider)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _jwtProvider = jwtProvider;
    }
    
    public async Task<User?> GetByUserNameAsync(string userName)
    {
        return await _userRepository.GetByUserNameAsync(userName);
    }

    public async Task<User?> GetByEmailAsync(string email)
    {
        return await _userRepository.GetByEmailAsync(email);
    }

    public async Task<User> RegisterUserAsync(RegisterUserRequest userRequest)
    {
        string hashedPassword = _passwordHasher.HashPassword(userRequest.Password);
        
        return await _userRepository.AddUserAsync(userRequest.ToUser(hashedPassword));
    }

    public async Task<(User? user, string? token)> LoginUserAsync(LoginUserRequest userRequest)
    {
        //Check the email and password

        var user = await _userRepository.GetByUserNameAsync(userRequest.UserName);

        if (user is null)
            return (null, null);
        
        var passwordCheck = _passwordHasher.Verify(userRequest.Password, user.PasswordHash);

        if (passwordCheck is false)
            return (null, null);
        
        //Create the JwtToken and return the JwtToken

        var token = _jwtProvider.GenerateToken(user);

        return (user, token);
    }
}