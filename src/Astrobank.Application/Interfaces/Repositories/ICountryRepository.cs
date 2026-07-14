using Astrobank.Application.Common.Models;
using Astrobank.Application.MasterData.Countries.Queries.ListCountries;
using Astrobank.Domain.Users;
namespace Astrobank.Application.Interfaces.Repositories;
public interface ICountryRepository {
    Task<PaginatedList<Country>> ListAsync(ListCountriesQuery query, CancellationToken cancellationToken = default);
    Task<Country?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task AddAsync(Country entity, CancellationToken cancellationToken = default);
    Task UpdateAsync(Country entity, CancellationToken cancellationToken = default);
}
