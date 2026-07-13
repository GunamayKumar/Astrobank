#!/bin/bash
entities=("ChartType" "ChartPermission" "EventType" "HelpCategory" "TagCategory" "Country")

for entity in "${entities[@]}"; do
    if [ "$entity" = "Country" ]; then
        dir="src/Astrobank.Application/MasterData/Countries"
    else
        dir="src/Astrobank.Application/MasterData/${entity}s"
    fi
    mkdir -p "$dir/DTOs"
    mkdir -p "$dir/Commands/Create${entity}"
    mkdir -p "$dir/Commands/Update${entity}"
    if [ "$entity" = "Country" ]; then
        mkdir -p "$dir/Queries/ListCountries"
    else
        mkdir -p "$dir/Queries/List${entity}s"
    fi
    mkdir -p "$dir/Mappings"
    mkdir -p "$dir/Validators"

    # DTO
    if [ "$entity" = "Country" ]; then
        cat << DTO_EOF > "$dir/DTOs/CountryDto.cs"
namespace Astrobank.Application.MasterData.Countries.DTOs;
public class CountryDto {
    public int CountryID { get; set; }
    public string CountryName { get; set; } = string.Empty;
    public string ISOCode2 { get; set; } = string.Empty;
    public string ISOCode3 { get; set; } = string.Empty;
    public string? PhoneCode { get; set; }
    public int DisplayOrder { get; set; }
    public bool IsActive { get; set; }
}
DTO_EOF
    else
        cat << DTO_EOF > "$dir/DTOs/${entity}Dto.cs"
namespace Astrobank.Application.MasterData.${entity}s.DTOs;
public class ${entity}Dto {
    public int ${entity}ID { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
$(if [ "$entity" = "EventType" ]; then echo "    public string? Category { get; set; }"; fi)
    public int DisplayOrder { get; set; }
    public bool IsActive { get; set; }
}
DTO_EOF
    fi

    # Query
    if [ "$entity" = "Country" ]; then
        cat << QUERY_EOF > "$dir/Queries/ListCountries/ListCountriesQuery.cs"
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

public class PaginatedList<T> {
    public List<T> Items { get; set; } = new();
    public int TotalCount { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
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
QUERY_EOF
    else
        cat << QUERY_EOF > "$dir/Queries/List${entity}s/List${entity}sQuery.cs"
using Astrobank.Application.Common.CQRS;
using Astrobank.Application.MasterData.${entity}s.DTOs;
using Astrobank.Application.MasterData.Countries.Queries.ListCountries;
using Astrobank.Application.Interfaces.Repositories;
using Astrobank.Domain.MasterData;
using AutoMapper;
using System.Collections.Generic;

namespace Astrobank.Application.MasterData.${entity}s.Queries.List${entity}s;

public class List${entity}sQuery {
    public string? SearchTerm { get; set; }
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    public string SortColumn { get; set; } = "DisplayOrder";
    public bool SortDescending { get; set; } = false;
}

public class List${entity}sQueryHandler : IQueryHandler<List${entity}sQuery, PaginatedList<${entity}Dto>> {
    private readonly I${entity}Repository _repository;
    private readonly IMapper _mapper;
    public List${entity}sQueryHandler(I${entity}Repository repository, IMapper mapper) {
        _repository = repository; _mapper = mapper;
    }
    public async Task<PaginatedList<${entity}Dto>> HandleAsync(List${entity}sQuery query, CancellationToken cancellationToken = default) {
        var result = await _repository.ListAsync(query, cancellationToken);
        return new PaginatedList<${entity}Dto> {
            Items = _mapper.Map<List<${entity}Dto>>(result.Items),
            TotalCount = result.TotalCount,
            PageNumber = result.PageNumber,
            PageSize = result.PageSize
        };
    }
}
QUERY_EOF
    fi

    # Create Command
    if [ "$entity" = "Country" ]; then
        cat << CMD_EOF > "$dir/Commands/CreateCountry/CreateCountryCommand.cs"
namespace Astrobank.Application.MasterData.Countries.Commands.CreateCountry;
public class CreateCountryCommand {
    public string CountryName { get; set; } = string.Empty;
    public string ISOCode2 { get; set; } = string.Empty;
    public string ISOCode3 { get; set; } = string.Empty;
    public string? PhoneCode { get; set; }
    public int DisplayOrder { get; set; }
    public bool IsActive { get; set; } = true;
}
CMD_EOF
    else
        cat << CMD_EOF > "$dir/Commands/Create${entity}/Create${entity}Command.cs"
namespace Astrobank.Application.MasterData.${entity}s.Commands.Create${entity};
public class Create${entity}Command {
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
$(if [ "$entity" = "EventType" ]; then echo "    public string? Category { get; set; }"; fi)
    public int DisplayOrder { get; set; }
    public bool IsActive { get; set; } = true;
}
CMD_EOF
    fi

    # Create Command Handler
    if [ "$entity" = "Country" ]; then
        cat << HANDLER_EOF > "$dir/Commands/CreateCountry/CreateCountryCommandHandler.cs"
using Astrobank.Application.Common.CQRS;
using Astrobank.Application.Interfaces.Repositories;
using Astrobank.Domain.Users;
namespace Astrobank.Application.MasterData.Countries.Commands.CreateCountry;
public class CreateCountryCommandHandler : ICommandHandler<CreateCountryCommand> {
    private readonly ICountryRepository _repository;
    public CreateCountryCommandHandler(ICountryRepository repository) => _repository = repository;
    public async Task HandleAsync(CreateCountryCommand command, CancellationToken cancellationToken = default) {
        var entity = new Country {
            CountryName = command.CountryName, ISOCode2 = command.ISOCode2, ISOCode3 = command.ISOCode3, PhoneCode = command.PhoneCode,
            DisplayOrder = command.DisplayOrder, IsActive = command.IsActive
        };
        entity.SetCreatedOn(DateTime.UtcNow);
        await _repository.AddAsync(entity, cancellationToken);
    }
}
HANDLER_EOF
    else
        cat << HANDLER_EOF > "$dir/Commands/Create${entity}/Create${entity}CommandHandler.cs"
using Astrobank.Application.Common.CQRS;
using Astrobank.Application.Interfaces.Repositories;
using Astrobank.Domain.MasterData;
namespace Astrobank.Application.MasterData.${entity}s.Commands.Create${entity};
public class Create${entity}CommandHandler : ICommandHandler<Create${entity}Command> {
    private readonly I${entity}Repository _repository;
    public Create${entity}CommandHandler(I${entity}Repository repository) => _repository = repository;
    public async Task HandleAsync(Create${entity}Command command, CancellationToken cancellationToken = default) {
        var entity = new ${entity} {
            Name = command.Name, Description = command.Description,
$(if [ "$entity" = "EventType" ]; then echo "            Category = command.Category,"; fi)
            DisplayOrder = command.DisplayOrder, IsActive = command.IsActive
        };
        entity.SetCreatedOn(DateTime.UtcNow);
        await _repository.AddAsync(entity, cancellationToken);
    }
}
HANDLER_EOF
    fi

    # Update Command
    if [ "$entity" = "Country" ]; then
        cat << UPD_EOF > "$dir/Commands/UpdateCountry/UpdateCountryCommand.cs"
namespace Astrobank.Application.MasterData.Countries.Commands.UpdateCountry;
public class UpdateCountryCommand {
    public int CountryID { get; set; }
    public string CountryName { get; set; } = string.Empty;
    public string ISOCode2 { get; set; } = string.Empty;
    public string ISOCode3 { get; set; } = string.Empty;
    public string? PhoneCode { get; set; }
    public int DisplayOrder { get; set; }
    public bool IsActive { get; set; }
}
UPD_EOF
    else
        cat << UPD_EOF > "$dir/Commands/Update${entity}/Update${entity}Command.cs"
namespace Astrobank.Application.MasterData.${entity}s.Commands.Update${entity};
public class Update${entity}Command {
    public int ${entity}ID { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
$(if [ "$entity" = "EventType" ]; then echo "    public string? Category { get; set; }"; fi)
    public int DisplayOrder { get; set; }
    public bool IsActive { get; set; }
}
UPD_EOF
    fi

    # Update Command Handler
    if [ "$entity" = "Country" ]; then
        cat << UPDH_EOF > "$dir/Commands/UpdateCountry/UpdateCountryCommandHandler.cs"
using Astrobank.Application.Common.CQRS;
using Astrobank.Application.Interfaces.Repositories;
using System.Collections.Generic;
namespace Astrobank.Application.MasterData.Countries.Commands.UpdateCountry;
public class UpdateCountryCommandHandler : ICommandHandler<UpdateCountryCommand> {
    private readonly ICountryRepository _repository;
    public UpdateCountryCommandHandler(ICountryRepository repository) => _repository = repository;
    public async Task HandleAsync(UpdateCountryCommand command, CancellationToken cancellationToken = default) {
        var entity = await _repository.GetByIdAsync(command.CountryID, cancellationToken);
        if (entity == null) throw new KeyNotFoundException($"Country with ID {command.CountryID} not found.");
        entity.CountryName = command.CountryName; entity.ISOCode2 = command.ISOCode2; entity.ISOCode3 = command.ISOCode3; entity.PhoneCode = command.PhoneCode;
        entity.DisplayOrder = command.DisplayOrder; entity.IsActive = command.IsActive; entity.MarkModified();
        await _repository.UpdateAsync(entity, cancellationToken);
    }
}
UPDH_EOF
    else
        cat << UPDH_EOF > "$dir/Commands/Update${entity}/Update${entity}CommandHandler.cs"
using Astrobank.Application.Common.CQRS;
using Astrobank.Application.Interfaces.Repositories;
using System.Collections.Generic;
namespace Astrobank.Application.MasterData.${entity}s.Commands.Update${entity};
public class Update${entity}CommandHandler : ICommandHandler<Update${entity}Command> {
    private readonly I${entity}Repository _repository;
    public Update${entity}CommandHandler(I${entity}Repository repository) => _repository = repository;
    public async Task HandleAsync(Update${entity}Command command, CancellationToken cancellationToken = default) {
        var entity = await _repository.GetByIdAsync(command.${entity}ID, cancellationToken);
        if (entity == null) throw new KeyNotFoundException($"${entity} with ID {command.${entity}ID} not found.");
        entity.Name = command.Name; entity.Description = command.Description;
$(if [ "$entity" = "EventType" ]; then echo "        entity.Category = command.Category;"; fi)
        entity.DisplayOrder = command.DisplayOrder; entity.IsActive = command.IsActive; entity.MarkModified();
        await _repository.UpdateAsync(entity, cancellationToken);
    }
}
UPDH_EOF
    fi

    # Mapping
    if [ "$entity" = "Country" ]; then
        cat << MAP_EOF > "$dir/Mappings/CountryProfile.cs"
using Astrobank.Application.MasterData.Countries.DTOs;
using Astrobank.Domain.Users;
using AutoMapper;
namespace Astrobank.Application.MasterData.Countries.Mappings;
public class CountryProfile : Profile {
    public CountryProfile() { CreateMap<Country, CountryDto>(); }
}
MAP_EOF
    else
        cat << MAP_EOF > "$dir/Mappings/${entity}Profile.cs"
using Astrobank.Application.MasterData.${entity}s.DTOs;
using Astrobank.Domain.MasterData;
using AutoMapper;
namespace Astrobank.Application.MasterData.${entity}s.Mappings;
public class ${entity}Profile : Profile {
    public ${entity}Profile() { CreateMap<${entity}, ${entity}Dto>(); }
}
MAP_EOF
    fi

    # Validators
    if [ "$entity" = "Country" ]; then
        cat << VAL1_EOF > "$dir/Validators/CreateCountryCommandValidator.cs"
using Astrobank.Application.MasterData.Countries.Commands.CreateCountry;
using FluentValidation;
namespace Astrobank.Application.MasterData.Countries.Validators;
public class CreateCountryCommandValidator : AbstractValidator<CreateCountryCommand> {
    public CreateCountryCommandValidator() {
        RuleFor(x => x.CountryName).NotEmpty().MaximumLength(200);
        RuleFor(x => x.ISOCode2).NotEmpty().Length(2); RuleFor(x => x.ISOCode3).NotEmpty().Length(3); RuleFor(x => x.PhoneCode).MaximumLength(10);
    }
}
VAL1_EOF
        cat << VAL2_EOF > "$dir/Validators/UpdateCountryCommandValidator.cs"
using Astrobank.Application.MasterData.Countries.Commands.UpdateCountry;
using FluentValidation;
namespace Astrobank.Application.MasterData.Countries.Validators;
public class UpdateCountryCommandValidator : AbstractValidator<UpdateCountryCommand> {
    public UpdateCountryCommandValidator() {
        RuleFor(x => x.CountryID).GreaterThan(0); RuleFor(x => x.CountryName).NotEmpty().MaximumLength(200);
        RuleFor(x => x.ISOCode2).NotEmpty().Length(2); RuleFor(x => x.ISOCode3).NotEmpty().Length(3); RuleFor(x => x.PhoneCode).MaximumLength(10);
    }
}
VAL2_EOF
    else
        cat << VAL1_EOF > "$dir/Validators/Create${entity}CommandValidator.cs"
using Astrobank.Application.MasterData.${entity}s.Commands.Create${entity};
using FluentValidation;
namespace Astrobank.Application.MasterData.${entity}s.Validators;
public class Create${entity}CommandValidator : AbstractValidator<Create${entity}Command> {
    public Create${entity}CommandValidator() {
        RuleFor(x => x.Name).NotEmpty().MaximumLength(200); RuleFor(x => x.Description).MaximumLength(500);
$(if [ "$entity" = "EventType" ]; then echo "        RuleFor(x => x.Category).MaximumLength(100);"; fi)
    }
}
VAL1_EOF
        cat << VAL2_EOF > "$dir/Validators/Update${entity}CommandValidator.cs"
using Astrobank.Application.MasterData.${entity}s.Commands.Update${entity};
using FluentValidation;
namespace Astrobank.Application.MasterData.${entity}s.Validators;
public class Update${entity}CommandValidator : AbstractValidator<Update${entity}Command> {
    public Update${entity}CommandValidator() {
        RuleFor(x => x.${entity}ID).GreaterThan(0); RuleFor(x => x.Name).NotEmpty().MaximumLength(200); RuleFor(x => x.Description).MaximumLength(500);
$(if [ "$entity" = "EventType" ]; then echo "        RuleFor(x => x.Category).MaximumLength(100);"; fi)
    }
}
VAL2_EOF
    fi

    # Repo Implementation
    if [ "$entity" = "Country" ]; then
        cat << REPO_EOF > "src/Astrobank.Persistence/Repositories/MasterData/CountryRepository.cs"
using Astrobank.Application.Interfaces.Repositories;
using Astrobank.Application.MasterData.Countries.Queries.ListCountries;
using Astrobank.Domain.Users;
using Microsoft.EntityFrameworkCore;
using System.Linq;
namespace Astrobank.Persistence.Repositories.MasterData;
public class CountryRepository : ICountryRepository {
    private readonly AstrobankDbContext _context;
    public CountryRepository(AstrobankDbContext context) => _context = context;
    public async Task<PaginatedList<Country>> ListAsync(ListCountriesQuery query, CancellationToken cancellationToken = default) {
        var dbQuery = _context.Countries.AsNoTracking();
        if (!string.IsNullOrWhiteSpace(query.SearchTerm)) dbQuery = dbQuery.Where(c => c.CountryName.Contains(query.SearchTerm));
        var totalCount = await dbQuery.CountAsync(cancellationToken);
        dbQuery = query.SortColumn.ToLower() switch { "name" => query.SortDescending ? dbQuery.OrderByDescending(c => c.CountryName) : dbQuery.OrderBy(c => c.CountryName), _ => query.SortDescending ? dbQuery.OrderByDescending(c => c.DisplayOrder) : dbQuery.OrderBy(c => c.DisplayOrder) };
        var items = await dbQuery.Skip((query.PageNumber - 1) * query.PageSize).Take(query.PageSize).ToListAsync(cancellationToken);
        return new PaginatedList<Country> { Items = items, TotalCount = totalCount, PageNumber = query.PageNumber, PageSize = query.PageSize };
    }
    public async Task<Country?> GetByIdAsync(int id, CancellationToken cancellationToken = default) => await _context.Countries.FirstOrDefaultAsync(c => c.CountryID == id, cancellationToken);
    public async Task AddAsync(Country entity, CancellationToken cancellationToken = default) { await _context.Countries.AddAsync(entity, cancellationToken); await _context.SaveChangesAsync(cancellationToken); }
    public async Task UpdateAsync(Country entity, CancellationToken cancellationToken = default) { _context.Countries.Update(entity); await _context.SaveChangesAsync(cancellationToken); }
}
REPO_EOF
    else
        cat << REPO_EOF > "src/Astrobank.Persistence/Repositories/MasterData/${entity}Repository.cs"
using Astrobank.Application.Interfaces.Repositories;
using Astrobank.Application.MasterData.Countries.Queries.ListCountries;
using Astrobank.Application.MasterData.${entity}s.Queries.List${entity}s;
using Astrobank.Domain.MasterData;
using Microsoft.EntityFrameworkCore;
using System.Linq;
namespace Astrobank.Persistence.Repositories.MasterData;
public class ${entity}Repository : I${entity}Repository {
    private readonly AstrobankDbContext _context;
    public ${entity}Repository(AstrobankDbContext context) => _context = context;
    public async Task<PaginatedList<${entity}>> ListAsync(List${entity}sQuery query, CancellationToken cancellationToken = default) {
        var dbQuery = _context.${entity}s.AsNoTracking();
        if (!string.IsNullOrWhiteSpace(query.SearchTerm)) dbQuery = dbQuery.Where(c => c.Name.Contains(query.SearchTerm));
        var totalCount = await dbQuery.CountAsync(cancellationToken);
        dbQuery = query.SortColumn.ToLower() switch { "name" => query.SortDescending ? dbQuery.OrderByDescending(c => c.Name) : dbQuery.OrderBy(c => c.Name), _ => query.SortDescending ? dbQuery.OrderByDescending(c => c.DisplayOrder) : dbQuery.OrderBy(c => c.DisplayOrder) };
        var items = await dbQuery.Skip((query.PageNumber - 1) * query.PageSize).Take(query.PageSize).ToListAsync(cancellationToken);
        return new PaginatedList<${entity}> { Items = items, TotalCount = totalCount, PageNumber = query.PageNumber, PageSize = query.PageSize };
    }
    public async Task<${entity}?> GetByIdAsync(int id, CancellationToken cancellationToken = default) => await _context.${entity}s.FirstOrDefaultAsync(c => c.${entity}ID == id, cancellationToken);
    public async Task AddAsync(${entity} entity, CancellationToken cancellationToken = default) { await _context.${entity}s.AddAsync(entity, cancellationToken); await _context.SaveChangesAsync(cancellationToken); }
    public async Task UpdateAsync(${entity} entity, CancellationToken cancellationToken = default) { _context.${entity}s.Update(entity); await _context.SaveChangesAsync(cancellationToken); }
}
REPO_EOF
    fi

    # Repo Interface
    mkdir -p src/Astrobank.Application/Interfaces/Repositories
    if [ "$entity" = "Country" ]; then
        cat << IREPO_EOF > "src/Astrobank.Application/Interfaces/Repositories/ICountryRepository.cs"
using Astrobank.Application.MasterData.Countries.Queries.ListCountries;
using Astrobank.Domain.Users;
namespace Astrobank.Application.Interfaces.Repositories;
public interface ICountryRepository {
    Task<PaginatedList<Country>> ListAsync(ListCountriesQuery query, CancellationToken cancellationToken = default);
    Task<Country?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task AddAsync(Country entity, CancellationToken cancellationToken = default);
    Task UpdateAsync(Country entity, CancellationToken cancellationToken = default);
}
IREPO_EOF
    else
        cat << IREPO_EOF > "src/Astrobank.Application/Interfaces/Repositories/I${entity}Repository.cs"
using Astrobank.Application.MasterData.Countries.Queries.ListCountries;
using Astrobank.Application.MasterData.${entity}s.Queries.List${entity}s;
using Astrobank.Domain.MasterData;
namespace Astrobank.Application.Interfaces.Repositories;
public interface I${entity}Repository {
    Task<PaginatedList<${entity}>> ListAsync(List${entity}sQuery query, CancellationToken cancellationToken = default);
    Task<${entity}?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task AddAsync(${entity} entity, CancellationToken cancellationToken = default);
    Task UpdateAsync(${entity} entity, CancellationToken cancellationToken = default);
}
IREPO_EOF
    fi
done
