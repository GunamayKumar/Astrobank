using Astrobank.Application.Common.CQRS;
using Astrobank.Application.Interfaces.Repositories;
using System.Collections.Generic;
namespace Astrobank.Application.MasterData.HelpCategorys.Commands.UpdateHelpCategory;
public class UpdateHelpCategoryCommandHandler : ICommandHandler<UpdateHelpCategoryCommand> {
    private readonly IHelpCategoryRepository _repository;
    public UpdateHelpCategoryCommandHandler(IHelpCategoryRepository repository) => _repository = repository;
    public async Task HandleAsync(UpdateHelpCategoryCommand command, CancellationToken cancellationToken = default) {
        var entity = await _repository.GetByIdAsync(command.HelpCategoryID, cancellationToken);
        if (entity == null) throw new KeyNotFoundException($"HelpCategory with ID {command.HelpCategoryID} not found.");
        entity.Name = command.Name; entity.Description = command.Description;

        entity.DisplayOrder = command.DisplayOrder; entity.IsActive = command.IsActive; entity.MarkModified();
        await _repository.UpdateAsync(entity, cancellationToken);
    }
}
