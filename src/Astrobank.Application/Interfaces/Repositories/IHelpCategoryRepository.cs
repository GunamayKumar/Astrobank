using Astrobank.Application.Common.Models;
using Astrobank.Application.MasterData.HelpCategorys.Queries.ListHelpCategorys;
using Astrobank.Domain.MasterData;
namespace Astrobank.Application.Interfaces.Repositories;
public interface IHelpCategoryRepository {
    Task<PaginatedList<HelpCategory>> ListAsync(ListHelpCategorysQuery query, CancellationToken cancellationToken = default);
    Task<HelpCategory?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task AddAsync(HelpCategory entity, CancellationToken cancellationToken = default);
    Task UpdateAsync(HelpCategory entity, CancellationToken cancellationToken = default);
}
