using Astrobank.Application.Common.Models;
using Astrobank.Application.Charts.DTOs;
using Astrobank.Application.Common.CQRS;
using Astrobank.Application.Interfaces.Repositories;
using Astrobank.Domain.Charts;
using AutoMapper;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Astrobank.Application.Charts.Queries.ListCharts;
public class ListChartsQuery {
    public string? SearchTerm { get; set; }
    public int? CountryID { get; set; }
    public int? ChartTypeID { get; set; }
    public int? ChartPermissionID { get; set; }
    public bool? AskingForHelp { get; set; }
    public List<int> TagIDs { get; set; } = new();
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    public string SortColumn { get; set; } = "CreatedOn";
    public bool SortDescending { get; set; } = true;
}
public class ListChartsQueryHandler : IQueryHandler<ListChartsQuery, PaginatedList<ChartDto>> {
    private readonly IChartRepository _repository;
    private readonly IMapper _mapper;
    public ListChartsQueryHandler(IChartRepository repository, IMapper mapper) {
        _repository = repository; _mapper = mapper;
    }
    public async Task<PaginatedList<ChartDto>> HandleAsync(ListChartsQuery query, CancellationToken cancellationToken = default) {
        var result = await _repository.ListAsync(query, cancellationToken);
        return new PaginatedList<ChartDto> {
            Items = _mapper.Map<List<ChartDto>>(result.Items),
            TotalCount = result.TotalCount,
            PageNumber = result.PageNumber,
            PageSize = result.PageSize
        };
    }
}
