using Astrobank.Application.Common.CQRS;
using Astrobank.Application.MasterData.HelpCategorys.DTOs;
using Astrobank.Application.MasterData.Countries.Queries.ListCountries;
using Astrobank.Application.Interfaces.Repositories;
using Astrobank.Domain.MasterData;
using AutoMapper;
using System.Collections.Generic;

namespace Astrobank.Application.MasterData.HelpCategorys.Queries.ListHelpCategorys;

public class ListHelpCategorysQuery {
    public string? SearchTerm { get; set; }
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    public string SortColumn { get; set; } = "DisplayOrder";
    public bool SortDescending { get; set; } = false;
}

public class ListHelpCategorysQueryHandler : IQueryHandler<ListHelpCategorysQuery, PaginatedList<HelpCategoryDto>> {
    private readonly IHelpCategoryRepository _repository;
    private readonly IMapper _mapper;
    public ListHelpCategorysQueryHandler(IHelpCategoryRepository repository, IMapper mapper) {
        _repository = repository; _mapper = mapper;
    }
    public async Task<PaginatedList<HelpCategoryDto>> HandleAsync(ListHelpCategorysQuery query, CancellationToken cancellationToken = default) {
        var result = await _repository.ListAsync(query, cancellationToken);
        return new PaginatedList<HelpCategoryDto> {
            Items = _mapper.Map<List<HelpCategoryDto>>(result.Items),
            TotalCount = result.TotalCount,
            PageNumber = result.PageNumber,
            PageSize = result.PageSize
        };
    }
}
