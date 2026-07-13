using Astrobank.Application.MasterData.Countries.Queries.ListCountries;
using Astrobank.Application.MasterData.ChartTypes.Queries.ListChartTypes;
using Astrobank.Domain.MasterData;
namespace Astrobank.Application.Interfaces.Repositories;
public interface IChartTypeRepository {
    Task<PaginatedList<ChartType>> ListAsync(ListChartTypesQuery query, CancellationToken cancellationToken = default);
    Task<ChartType?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task AddAsync(ChartType entity, CancellationToken cancellationToken = default);
    Task UpdateAsync(ChartType entity, CancellationToken cancellationToken = default);
}
