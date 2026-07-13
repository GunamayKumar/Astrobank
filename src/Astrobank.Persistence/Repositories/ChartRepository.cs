using Astrobank.Application.Charts.Queries.ListCharts;
using Astrobank.Application.Interfaces.Repositories;
using Astrobank.Domain.Charts;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Astrobank.Persistence.Repositories;

public class ChartRepository : IChartRepository {
    private readonly AstrobankDbContext _context;
    public ChartRepository(AstrobankDbContext context) => _context = context;

    public async Task<PaginatedList<Chart>> ListAsync(ListChartsQuery query, CancellationToken cancellationToken = default) {
        var dbQuery = _context.Charts.Include(c => c.User).Include(c => c.ChartType).Include(c => c.ChartPermission).Include(c => c.Country).AsNoTracking();
        if (!string.IsNullOrWhiteSpace(query.SearchTerm)) dbQuery = dbQuery.Where(c => c.Name.Contains(query.SearchTerm) || (c.Alias != null && c.Alias.Contains(query.SearchTerm)) || c.BirthPlace.Contains(query.SearchTerm));
        if (query.CountryID.HasValue) dbQuery = dbQuery.Where(c => c.CountryID == query.CountryID.Value);
        if (query.ChartTypeID.HasValue) dbQuery = dbQuery.Where(c => c.ChartTypeID == query.ChartTypeID.Value);
        if (query.ChartPermissionID.HasValue) dbQuery = dbQuery.Where(c => c.ChartPermissionID == query.ChartPermissionID.Value);
        if (query.AskingForHelp.HasValue) dbQuery = dbQuery.Where(c => c.AskingForHelp == query.AskingForHelp.Value);
        if (query.TagIDs != null && query.TagIDs.Any()) dbQuery = dbQuery.Where(c => c.ChartTags.Any(ct => query.TagIDs.Contains(ct.TagID)));
        var totalCount = await dbQuery.CountAsync(cancellationToken);
        dbQuery = query.SortColumn.ToLower() switch { "name" => query.SortDescending ? dbQuery.OrderByDescending(c => c.Name) : dbQuery.OrderBy(c => c.Name), _ => query.SortDescending ? dbQuery.OrderByDescending(c => c.CreatedOn) : dbQuery.OrderBy(c => c.CreatedOn) };
        var items = await dbQuery.Skip((query.PageNumber - 1) * query.PageSize).Take(query.PageSize).ToListAsync(cancellationToken);
        return new PaginatedList<Chart> { Items = items, TotalCount = totalCount, PageNumber = query.PageNumber, PageSize = query.PageSize };
    }
    public async Task<Chart?> GetByIdAsync(int id, CancellationToken cancellationToken = default) => await _context.Charts.Include(c => c.User).Include(c => c.ChartType).Include(c => c.ChartPermission).Include(c => c.Country).Include(c => c.ChartTags).ThenInclude(ct => ct.Tag).Include(c => c.ChartEvents).ThenInclude(ce => ce.EventType).Include(c => c.ChartImages).ThenInclude(ci => ci.ChartImageType).Include(c => c.ChartFiles).FirstOrDefaultAsync(c => c.ChartID == id, cancellationToken);
    public async Task AddAsync(Chart chart, CancellationToken cancellationToken = default) { await _context.Charts.AddAsync(chart, cancellationToken); await _context.SaveChangesAsync(cancellationToken); }
    public async Task UpdateAsync(Chart chart, CancellationToken cancellationToken = default) { _context.Charts.Update(chart); await _context.SaveChangesAsync(cancellationToken); }
    public async Task DeleteAsync(Chart chart, CancellationToken cancellationToken = default) { chart.IsDeleted = true; _context.Charts.Update(chart); await _context.SaveChangesAsync(cancellationToken); }
}
