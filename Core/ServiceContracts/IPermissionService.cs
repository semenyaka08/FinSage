namespace Core.ServiceContracts;

public interface IPermissionService
{
    Task<List<string>?> GetUsersPermissions(Guid userId);
}