using Astrobank.Application.Common.Models;
using Astrobank.Application.Common.Models;
using Astrobank.Application.Interfaces.Repositories;
using Astrobank.Application.MasterData.TagCategorys.Queries.ListTagCategorys;
using Astrobank.Domain.MasterData;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Astrobank.Persistence.Repositories;

public class TagCategoryRepository : ITagCategoryRepository
{
    private readonly AstrobankDbContext _context;

    public TagCategoryRepository(AstrobankDbContext context)
    {
        _context = context;
    }

    public async Task<PaginatedList<TagCategory>> ListAsync(ListTagCategorysQuery query, CancellationToken cancellationToken = default)
    {
        var dbQuery = _context.TagCategories.AsNoTracking();

        if (!string.IsNullOrWhiteSpace(query.SearchTerm))
        {
            dbQuery = dbQuery.Where(c => c.Name.Contains(query.SearchTerm));
        }

        var totalCount = await dbQuery.CountAsync(cancellationToken);

        dbQuery = query.SortColumn?.ToLower() switch { "name" => query.SortDescending ? dbQuery.OrderByDescending(c => c.Name) : dbQuery.OrderBy(c => c.Name), _ => query.SortDescending ? dbQuery.OrderByDescending(c => c.DisplayOrder) : dbQuery.OrderBy(c => c.DisplayOrder) };

        var items = await dbQuery
            .Skip((query.PageNumber - 1) * query.PageSize)
            .Take(query.PageSize)
            .ToListAsync(cancellationToken);

        return new PaginatedList<TagCategory>
        {
            Items = items,
            TotalCount = totalCount,
            PageNumber = query.PageNumber,
            PageSize = query.PageSize
        };
    }

    public async Task<TagCategory?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _context.TagCategories.FirstOrDefaultAsync(c => c.TagCategoryID == id, cancellationToken);
    }

    public async Task AddAsync(TagCategory entity, CancellationToken cancellationToken = default)
    {
        await _context.TagCategories.AddAsync(entity, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(TagCategory entity, CancellationToken cancellationToken = default)
    {
        _context.TagCategories.Update(entity);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
