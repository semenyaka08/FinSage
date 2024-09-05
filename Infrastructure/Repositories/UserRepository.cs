using Core.Domain.Models;
using Core.Domain.RepositoryContracts;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _context;

    public UserRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<User?> GetByUserNameAsync(string userName)
    {
        return await _context.Users.FirstOrDefaultAsync(z=>z.UserName == userName);
    }

    public async Task<User> AddUserAsync(User user)
    {
        user.RoleId = 2;
        
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
        
        return user;
    }

    public async Task<User?> GetByEmailAsync(string email)
    {
        return await _context.Users.FirstOrDefaultAsync(z => z.Email == email);
    }

    public async Task<List<string>?> GetUsersPermissions(Guid userId)
    {
        var result = await _context.Users
            .Include(u => u.Role)
            .ThenInclude(r => r.Permissions)
            .Where(u => u.Id == userId)
            .SelectMany(u => u.Role.Permissions)
            .Select(p => p.Name)
            .ToListAsync();

        return result;
    }
}