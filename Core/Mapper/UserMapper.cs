using Core.Domain.Models;
using Core.DTO.Identity;

namespace Core.Mapper;

public static class UserMapper
{
    public static User ToUser(this RegisterUserRequest userRequest, string passwordHash)
    {
        return new User
        {
            UserName = userRequest.UserName,
            Email = userRequest.Email,
            PasswordHash = passwordHash,
        };
    }
    
    public static RegisterUserResponse ToUserRegisterResponse(this User userRequest)
    {
        return new RegisterUserResponse(userRequest.UserName, userRequest.Email);
    }
}