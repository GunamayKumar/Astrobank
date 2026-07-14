using Astrobank.Application.Common.Models;
using Astrobank.Application.Common.CQRS;
using Astrobank.Application.MasterData.EventTypes.DTOs;
using Astrobank.Application.Interfaces.Repositories;
using Astrobank.Domain.MasterData;
using AutoMapper;
using System.Collections.Generic;

namespace Astrobank.Application.MasterData.EventTypes.Queries.ListEventTypes;

public class ListEventTypesQuery {
    public string? SearchTerm { get; set; }
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    public string SortColumn { get; set; } = "DisplayOrder";
    public bool SortDescending { get; set; } = false;
}

public class ListEventTypesQueryHandler : IQueryHandler<ListEventTypesQuery, PaginatedList<EventTypeDto>> {
    private readonly IEventTypeRepository _repository;
    private readonly IMapper _mapper;
    public ListEventTypesQueryHandler(IEventTypeRepository repository, IMapper mapper) {
        _repository = repository; _mapper = mapper;
    }
    public async Task<PaginatedList<EventTypeDto>> HandleAsync(ListEventTypesQuery query, CancellationToken cancellationToken = default) {
        var result = await _repository.ListAsync(query, cancellationToken);
        return new PaginatedList<EventTypeDto> {
            Items = _mapper.Map<List<EventTypeDto>>(result.Items),
            TotalCount = result.TotalCount,
            PageNumber = result.PageNumber,
            PageSize = result.PageSize
        };
    }
}
