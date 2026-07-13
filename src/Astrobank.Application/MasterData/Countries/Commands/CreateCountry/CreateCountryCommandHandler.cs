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
