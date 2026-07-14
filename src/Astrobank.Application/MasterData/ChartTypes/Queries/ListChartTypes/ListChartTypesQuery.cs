using Astrobank.Application.Common.Models;
using Astrobank.Application.Common.CQRS;
using Astrobank.Application.MasterData.ChartTypes.DTOs;
using Astrobank.Application.Interfaces.Repositories;
using Astrobank.Domain.MasterData;
using AutoMapper;
using System.Collections.Generic;

namespace Astrobank.Application.MasterData.ChartTypes.Queries.ListChartTypes;

public class ListChartTypesQuery {
    public string? SearchTerm { get; set; }
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    public string SortColumn { get; set; } = "DisplayOrder";
    public bool SortDescending { get; set; } = false;
}

public class ListChartTypesQueryHandler : IQueryHandler<ListChartTypesQuery, PaginatedList<ChartTypeDto>> {
    private readonly IChartTypeRepository _repository;
    private readonly IMapper _mapper;
    public ListChartTypesQueryHandler(IChartTypeRepository repository, IMapper mapper) {
        _repository = repository; _mapper = mapper;
    }
    public async Task<PaginatedList<ChartTypeDto>> HandleAsync(ListChartTypesQuery query, CancellationToken cancellationToken = default) {
        var result = await _repository.ListAsync(query, cancellationToken);
        return new PaginatedList<ChartTypeDto> {
            Items = _mapper.Map<List<ChartTypeDto>>(result.Items),
            TotalCount = result.TotalCount,
            PageNumber = result.PageNumber,
            PageSize = result.PageSize
        };
    }
}
