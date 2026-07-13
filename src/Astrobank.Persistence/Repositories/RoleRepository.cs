using Astrobank.Application.Interfaces.Repositories;
using Astrobank.Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace Astrobank.Persistence.Repositories;

public class RoleRepository : IRoleRepository
{
    private readonly AstrobankDbContext _dbContext;

    public RoleRepository(AstrobankDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Role?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Roles.FirstOrDefaultAsync(r => r.RoleID == id, cancellationToken);
    }

    public async Task<Role?> GetByNameAsync(string name, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Roles.FirstOrDefaultAsync(r => r.Name == name, cancellationToken);
    }

    public async Task AddAsync(Role role, CancellationToken cancellationToken = default)
    {
        await _dbContext.Roles.AddAsync(role, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Role role, CancellationToken cancellationToken = default)
    {
        _dbContext.Roles.Update(role);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Role role, CancellationToken cancellationToken = default)
    {
        _dbContext.Roles.Remove(role);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
