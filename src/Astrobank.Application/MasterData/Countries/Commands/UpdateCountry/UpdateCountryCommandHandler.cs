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
