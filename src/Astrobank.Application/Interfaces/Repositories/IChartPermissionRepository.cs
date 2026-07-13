using Astrobank.Application.MasterData.Countries.Queries.ListCountries;
using Astrobank.Application.MasterData.ChartPermissions.Queries.ListChartPermissions;
using Astrobank.Domain.MasterData;
namespace Astrobank.Application.Interfaces.Repositories;
public interface IChartPermissionRepository {
    Task<PaginatedList<ChartPermission>> ListAsync(ListChartPermissionsQuery query, CancellationToken cancellationToken = default);
    Task<ChartPermission?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task AddAsync(ChartPermission entity, CancellationToken cancellationToken = default);
    Task UpdateAsync(ChartPermission entity, CancellationToken cancellationToken = default);
}
