using Astrobank.Application.Common.Models;
using Astrobank.Application.MasterData.TagCategorys.Queries.ListTagCategorys;
using Astrobank.Domain.MasterData;
namespace Astrobank.Application.Interfaces.Repositories;
public interface ITagCategoryRepository {
    Task<PaginatedList<TagCategory>> ListAsync(ListTagCategorysQuery query, CancellationToken cancellationToken = default);
    Task<TagCategory?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task AddAsync(TagCategory entity, CancellationToken cancellationToken = default);
    Task UpdateAsync(TagCategory entity, CancellationToken cancellationToken = default);
}
