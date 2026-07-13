using Astrobank.Application.Common.CQRS;
using Astrobank.Application.Interfaces.Repositories;
using Astrobank.Domain.MasterData;
namespace Astrobank.Application.MasterData.EventTypes.Commands.CreateEventType;
public class CreateEventTypeCommandHandler : ICommandHandler<CreateEventTypeCommand> {
    private readonly IEventTypeRepository _repository;
    public CreateEventTypeCommandHandler(IEventTypeRepository repository) => _repository = repository;
    public async Task HandleAsync(CreateEventTypeCommand command, CancellationToken cancellationToken = default) {
        var entity = new EventType {
            Name = command.Name, Description = command.Description,
            Category = command.Category,
            DisplayOrder = command.DisplayOrder, IsActive = command.IsActive
        };
        entity.SetCreatedOn(DateTime.UtcNow);
        await _repository.AddAsync(entity, cancellationToken);
    }
}
