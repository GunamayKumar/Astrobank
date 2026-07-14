using Astrobank.Application.Common.Models;
using Astrobank.Application.Common.CQRS;
using Astrobank.Application.MasterData.Countries.DTOs;
using Astrobank.Application.Interfaces.Repositories;
using Astrobank.Domain.Users;
using AutoMapper;
using System.Collections.Generic;

namespace Astrobank.Application.MasterData.Countries.Queries.ListCountries;

public class ListCountriesQuery {
    public string? SearchTerm { get; set; }
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    public string SortColumn { get; set; } = "DisplayOrder";
    public bool SortDescending { get; set; } = false;
}


public class ListCountriesQueryHandler : IQueryHandler<ListCountriesQuery, PaginatedList<CountryDto>> {
    private readonly ICountryRepository _repository;
    private readonly IMapper _mapper;
    public ListCountriesQueryHandler(ICountryRepository repository, IMapper mapper) {
        _repository = repository; _mapper = mapper;
    }
    public async Task<PaginatedList<CountryDto>> HandleAsync(ListCountriesQuery query, CancellationToken cancellationToken = default) {
        var result = await _repository.ListAsync(query, cancellationToken);
        return new PaginatedList<CountryDto> {
            Items = _mapper.Map<List<CountryDto>>(result.Items),
            TotalCount = result.TotalCount,
            PageNumber = result.PageNumber,
            PageSize = result.PageSize
        };
    }
}
