using Astrobank.Application.Charts.Queries.ListCharts;
using Astrobank.Domain.Charts;
namespace Astrobank.Application.Interfaces.Repositories;
public interface IChartRepository {
    Task<PaginatedList<Chart>> ListAsync(ListChartsQuery query, CancellationToken cancellationToken = default);
    Task<Chart?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task AddAsync(Chart chart, CancellationToken cancellationToken = default);
    Task UpdateAsync(Chart chart, CancellationToken cancellationToken = default);
    Task DeleteAsync(Chart chart, CancellationToken cancellationToken = default);
}
