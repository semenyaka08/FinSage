using Core.ServiceContracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Authentication;

public class PermissionAuthorizationHandler : AuthorizationHandler<PermissionRequirement>
{
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public PermissionAuthorizationHandler(IServiceScopeFactory serviceScopeFactory)
    {
        _serviceScopeFactory = serviceScopeFactory;
    }
    
    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
    {
        var userId = context.User.Claims.FirstOrDefault(z=>z.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")?.Value;

        if(!Guid.TryParse(userId, out Guid parsedId))
            return;

        using IServiceScope scope = _serviceScopeFactory.CreateScope();

        var service = scope.ServiceProvider.GetRequiredService<IPermissionService>();
        
        var permissions = await service.GetUsersPermissions(parsedId);
        
        if(permissions is null)
            return;
        
        if(permissions.Contains(requirement.Permission))
            context.Succeed(requirement);
    }
}