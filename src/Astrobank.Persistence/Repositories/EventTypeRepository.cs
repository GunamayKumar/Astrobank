using Astrobank.Application.Common.Models;
using Astrobank.Application.Common.Models;
using Astrobank.Application.Interfaces.Repositories;
using Astrobank.Application.MasterData.EventTypes.Queries.ListEventTypes;
using Astrobank.Domain.MasterData;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Astrobank.Persistence.Repositories;

public class EventTypeRepository : IEventTypeRepository
{
    private readonly AstrobankDbContext _context;

    public EventTypeRepository(AstrobankDbContext context)
    {
        _context = context;
    }

    public async Task<PaginatedList<EventType>> ListAsync(ListEventTypesQuery query, CancellationToken cancellationToken = default)
    {
        var dbQuery = _context.EventTypes.AsNoTracking();

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

        return new PaginatedList<EventType>
        {
            Items = items,
            TotalCount = totalCount,
            PageNumber = query.PageNumber,
            PageSize = query.PageSize
        };
    }

    public async Task<EventType?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _context.EventTypes.FirstOrDefaultAsync(c => c.EventTypeID == id, cancellationToken);
    }

    public async Task AddAsync(EventType entity, CancellationToken cancellationToken = default)
    {
        await _context.EventTypes.AddAsync(entity, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(EventType entity, CancellationToken cancellationToken = default)
    {
        _context.EventTypes.Update(entity);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
