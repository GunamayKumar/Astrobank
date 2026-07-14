using Astrobank.Application.Common.Models;
using Astrobank.Application.Common.Models;
using Astrobank.Application.Interfaces.Repositories;
using Astrobank.Application.MasterData.HelpCategorys.Queries.ListHelpCategorys;
using Astrobank.Domain.MasterData;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Astrobank.Persistence.Repositories;

public class HelpCategoryRepository : IHelpCategoryRepository
{
    private readonly AstrobankDbContext _context;

    public HelpCategoryRepository(AstrobankDbContext context)
    {
        _context = context;
    }

    public async Task<PaginatedList<HelpCategory>> ListAsync(ListHelpCategorysQuery query, CancellationToken cancellationToken = default)
    {
        var dbQuery = _context.HelpCategories.AsNoTracking();

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

        return new PaginatedList<HelpCategory>
        {
            Items = items,
            TotalCount = totalCount,
            PageNumber = query.PageNumber,
            PageSize = query.PageSize
        };
    }

    public async Task<HelpCategory?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _context.HelpCategories.FirstOrDefaultAsync(c => c.HelpCategoryID == id, cancellationToken);
    }

    public async Task AddAsync(HelpCategory entity, CancellationToken cancellationToken = default)
    {
        await _context.HelpCategories.AddAsync(entity, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(HelpCategory entity, CancellationToken cancellationToken = default)
    {
        _context.HelpCategories.Update(entity);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
