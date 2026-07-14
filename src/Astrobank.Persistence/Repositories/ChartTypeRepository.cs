using Astrobank.Application.Common.Models;
using Astrobank.Application.Common.Models;
using Astrobank.Application.Interfaces.Repositories;
using Astrobank.Application.MasterData.ChartTypes.Queries.ListChartTypes;
using Astrobank.Domain.MasterData;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Astrobank.Persistence.Repositories;

public class ChartTypeRepository : IChartTypeRepository
{
    private readonly AstrobankDbContext _context;

    public ChartTypeRepository(AstrobankDbContext context)
    {
        _context = context;
    }

    public async Task<PaginatedList<ChartType>> ListAsync(ListChartTypesQuery query, CancellationToken cancellationToken = default)
    {
        var dbQuery = _context.ChartTypes.AsNoTracking();

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

        return new PaginatedList<ChartType>
        {
            Items = items,
            TotalCount = totalCount,
            PageNumber = query.PageNumber,
            PageSize = query.PageSize
        };
    }

    public async Task<ChartType?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _context.ChartTypes.FirstOrDefaultAsync(c => c.ChartTypeID == id, cancellationToken);
    }

    public async Task AddAsync(ChartType entity, CancellationToken cancellationToken = default)
    {
        await _context.ChartTypes.AddAsync(entity, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(ChartType entity, CancellationToken cancellationToken = default)
    {
        _context.ChartTypes.Update(entity);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
