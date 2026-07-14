using Astrobank.Application.Common.Models;
using Astrobank.Application.Common.Models;
using Astrobank.Application.Interfaces.Repositories;
using Astrobank.Application.MasterData.Countries.Queries.ListCountries;
using Astrobank.Domain.Users;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Astrobank.Persistence.Repositories;

public class CountryRepository : ICountryRepository
{
    private readonly AstrobankDbContext _context;

    public CountryRepository(AstrobankDbContext context)
    {
        _context = context;
    }

    public async Task<PaginatedList<Country>> ListAsync(ListCountriesQuery query, CancellationToken cancellationToken = default)
    {
        var dbQuery = _context.Countries.AsNoTracking();

        if (!string.IsNullOrWhiteSpace(query.SearchTerm))
        {
            dbQuery = dbQuery.Where(c => c.CountryName.Contains(query.SearchTerm));
        }

        var totalCount = await dbQuery.CountAsync(cancellationToken);

        dbQuery = query.SortColumn?.ToLower() switch { "name" => query.SortDescending ? dbQuery.OrderByDescending(c => c.CountryName) : dbQuery.OrderBy(c => c.CountryName), "iso2" => query.SortDescending ? dbQuery.OrderByDescending(c => c.ISOCode2) : dbQuery.OrderBy(c => c.ISOCode2), _ => query.SortDescending ? dbQuery.OrderByDescending(c => c.DisplayOrder) : dbQuery.OrderBy(c => c.DisplayOrder) };

        var items = await dbQuery
            .Skip((query.PageNumber - 1) * query.PageSize)
            .Take(query.PageSize)
            .ToListAsync(cancellationToken);

        return new PaginatedList<Country>
        {
            Items = items,
            TotalCount = totalCount,
            PageNumber = query.PageNumber,
            PageSize = query.PageSize
        };
    }

    public async Task<Country?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _context.Countries.FirstOrDefaultAsync(c => c.CountryID == id, cancellationToken);
    }

    public async Task AddAsync(Country entity, CancellationToken cancellationToken = default)
    {
        await _context.Countries.AddAsync(entity, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Country entity, CancellationToken cancellationToken = default)
    {
        _context.Countries.Update(entity);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
