using Astrobank.Application.Common.CQRS;
using Astrobank.Application.Interfaces.Repositories;
using System.Collections.Generic;
namespace Astrobank.Application.MasterData.EventTypes.Commands.UpdateEventType;
public class UpdateEventTypeCommandHandler : ICommandHandler<UpdateEventTypeCommand> {
    private readonly IEventTypeRepository _repository;
    public UpdateEventTypeCommandHandler(IEventTypeRepository repository) => _repository = repository;
    public async Task HandleAsync(UpdateEventTypeCommand command, CancellationToken cancellationToken = default) {
        var entity = await _repository.GetByIdAsync(command.EventTypeID, cancellationToken);
        if (entity == null) throw new KeyNotFoundException($"EventType with ID {command.EventTypeID} not found.");
        entity.Name = command.Name; entity.Description = command.Description;
        entity.Category = command.Category;
        entity.DisplayOrder = command.DisplayOrder; entity.IsActive = command.IsActive; entity.MarkModified();
        await _repository.UpdateAsync(entity, cancellationToken);
    }
}
