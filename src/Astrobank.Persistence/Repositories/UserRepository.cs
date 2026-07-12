using Astrobank.Application.Interfaces.Repositories;
using Astrobank.Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace Astrobank.Persistence.Repositories;

public class UserRepository : IUserRepository
{
    private readonly AstrobankDbContext _dbContext;

    public UserRepository(AstrobankDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<User?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Users
            .Include(u => u.Role)
            .FirstOrDefaultAsync(u => u.UserID == id, cancellationToken);
    }

    public async Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Users
            .Include(u => u.Role)
            .FirstOrDefaultAsync(u => u.Email == email, cancellationToken);
    }

    public async Task<User?> GetByUsernameAsync(string username, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Users
            .Include(u => u.Role)
            .FirstOrDefaultAsync(u => u.Username == username, cancellationToken);
    }

    public async Task AddAsync(User user, CancellationToken cancellationToken = default)
    {
        await _dbContext.Users.AddAsync(user, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(User user, CancellationToken cancellationToken = default)
    {
        _dbContext.Users.Update(user);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(User user, CancellationToken cancellationToken = default)
    {
        _dbContext.Users.Remove(user);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
