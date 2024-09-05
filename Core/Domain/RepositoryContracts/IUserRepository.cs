using Core.Domain.Models;

namespace Core.Domain.RepositoryContracts;

public interface IUserRepository
{
    Task<User?> GetByUserNameAsync(string userName);

    Task<User> AddUserAsync(User user);

    Task<User?> GetByEmailAsync(string email);

    Task<List<string>?> GetUsersPermissions(Guid userId);
}