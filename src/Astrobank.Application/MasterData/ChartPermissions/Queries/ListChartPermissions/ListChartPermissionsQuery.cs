using Astrobank.Application.Common.CQRS;
using Astrobank.Application.MasterData.ChartPermissions.DTOs;
using Astrobank.Application.MasterData.Countries.Queries.ListCountries;
using Astrobank.Application.Interfaces.Repositories;
using Astrobank.Domain.MasterData;
using AutoMapper;
using System.Collections.Generic;

namespace Astrobank.Application.MasterData.ChartPermissions.Queries.ListChartPermissions;

public class ListChartPermissionsQuery {
    public string? SearchTerm { get; set; }
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    public string SortColumn { get; set; } = "DisplayOrder";
    public bool SortDescending { get; set; } = false;
}

public class ListChartPermissionsQueryHandler : IQueryHandler<ListChartPermissionsQuery, PaginatedList<ChartPermissionDto>> {
    private readonly IChartPermissionRepository _repository;
    private readonly IMapper _mapper;
    public ListChartPermissionsQueryHandler(IChartPermissionRepository repository, IMapper mapper) {
        _repository = repository; _mapper = mapper;
    }
    public async Task<PaginatedList<ChartPermissionDto>> HandleAsync(ListChartPermissionsQuery query, CancellationToken cancellationToken = default) {
        var result = await _repository.ListAsync(query, cancellationToken);
        return new PaginatedList<ChartPermissionDto> {
            Items = _mapper.Map<List<ChartPermissionDto>>(result.Items),
            TotalCount = result.TotalCount,
            PageNumber = result.PageNumber,
            PageSize = result.PageSize
        };
    }
}
