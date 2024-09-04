using Core.Domain.Models;

namespace Core.Abstraction;

public interface IJwtProvider
{
    string GenerateToken(User user);
}