using Astrobank.Application.Common.CQRS;
using Astrobank.Application.MasterData.TagCategorys.DTOs;
using Astrobank.Application.MasterData.Countries.Queries.ListCountries;
using Astrobank.Application.Interfaces.Repositories;
using Astrobank.Domain.MasterData;
using AutoMapper;
using System.Collections.Generic;

namespace Astrobank.Application.MasterData.TagCategorys.Queries.ListTagCategorys;

public class ListTagCategorysQuery {
    public string? SearchTerm { get; set; }
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    public string SortColumn { get; set; } = "DisplayOrder";
    public bool SortDescending { get; set; } = false;
}

public class ListTagCategorysQueryHandler : IQueryHandler<ListTagCategorysQuery, PaginatedList<TagCategoryDto>> {
    private readonly ITagCategoryRepository _repository;
    private readonly IMapper _mapper;
    public ListTagCategorysQueryHandler(ITagCategoryRepository repository, IMapper mapper) {
        _repository = repository; _mapper = mapper;
    }
    public async Task<PaginatedList<TagCategoryDto>> HandleAsync(ListTagCategorysQuery query, CancellationToken cancellationToken = default) {
        var result = await _repository.ListAsync(query, cancellationToken);
        return new PaginatedList<TagCategoryDto> {
            Items = _mapper.Map<List<TagCategoryDto>>(result.Items),
            TotalCount = result.TotalCount,
            PageNumber = result.PageNumber,
            PageSize = result.PageSize
        };
    }
}
