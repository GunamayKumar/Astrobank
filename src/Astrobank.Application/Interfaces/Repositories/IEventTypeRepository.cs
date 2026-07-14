using Astrobank.Application.Common.Models;
using Astrobank.Application.MasterData.EventTypes.Queries.ListEventTypes;
using Astrobank.Domain.MasterData;
namespace Astrobank.Application.Interfaces.Repositories;
public interface IEventTypeRepository {
    Task<PaginatedList<EventType>> ListAsync(ListEventTypesQuery query, CancellationToken cancellationToken = default);
    Task<EventType?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task AddAsync(EventType entity, CancellationToken cancellationToken = default);
    Task UpdateAsync(EventType entity, CancellationToken cancellationToken = default);
}
