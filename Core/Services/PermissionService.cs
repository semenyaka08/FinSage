using Core.Domain.RepositoryContracts;
using Core.ServiceContracts;

namespace Core.Services;

public class PermissionService : IPermissionService
{
    private readonly IUserRepository _userRepository;
    
    public PermissionService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    
    public async Task<List<string>?> GetUsersPermissions(Guid userId)
    {
        return  await _userRepository.GetUsersPermissions(userId);
    }
}