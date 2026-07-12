using Astrobank.Application.Interfaces.Repositories;
using Astrobank.Domain.Users;
using Microsoft.AspNetCore.Identity;

namespace Astrobank.Infrastructure.Identity;

public class AstrobankRoleStore : IRoleStore<Role>
{
    private readonly IRoleRepository _roleRepository;

    public AstrobankRoleStore(IRoleRepository roleRepository)
    {
        _roleRepository = roleRepository;
    }

    public void Dispose()
    {
        // No unmanaged resources
    }

    public Task<string> GetRoleIdAsync(Role role, CancellationToken cancellationToken)
    {
        return Task.FromResult(role.RoleID.ToString());
    }

    public Task<string?> GetRoleNameAsync(Role role, CancellationToken cancellationToken)
    {
        return Task.FromResult(role.Name)!;
    }

    public Task SetRoleNameAsync(Role role, string? roleName, CancellationToken cancellationToken)
    {
        if (roleName != null)
        {
            typeof(Role).GetProperty(nameof(Role.Name))!.SetValue(role, roleName);
        }
        return Task.CompletedTask;
    }

    public Task<string?> GetNormalizedRoleNameAsync(Role role, CancellationToken cancellationToken)
    {
        return Task.FromResult(role.Name.ToUpperInvariant())!;
    }

    public Task SetNormalizedRoleNameAsync(Role role, string? normalizedName, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    public async Task<IdentityResult> CreateAsync(Role role, CancellationToken cancellationToken)
    {
        await _roleRepository.AddAsync(role, cancellationToken);
        return IdentityResult.Success;
    }

    public async Task<IdentityResult> UpdateAsync(Role role, CancellationToken cancellationToken)
    {
        await _roleRepository.UpdateAsync(role, cancellationToken);
        return IdentityResult.Success;
    }

    public async Task<IdentityResult> DeleteAsync(Role role, CancellationToken cancellationToken)
    {
        if (role.IsSystemRole)
        {
            return IdentityResult.Failed(new IdentityError { Description = "Cannot delete a system role." });
        }

        await _roleRepository.DeleteAsync(role, cancellationToken);
        return IdentityResult.Success;
    }

    public async Task<Role?> FindByIdAsync(string roleId, CancellationToken cancellationToken)
    {
        if (int.TryParse(roleId, out int id))
        {
            return await _roleRepository.GetByIdAsync(id, cancellationToken);
        }
        return null;
    }

    public async Task<Role?> FindByNameAsync(string normalizedRoleName, CancellationToken cancellationToken)
    {
        return await _roleRepository.GetByNameAsync(normalizedRoleName, cancellationToken);
    }
}
