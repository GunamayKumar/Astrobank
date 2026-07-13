using Astrobank.Application.Common.CQRS;
using Astrobank.Application.Interfaces.Repositories;
using System.Collections.Generic;
namespace Astrobank.Application.MasterData.TagCategorys.Commands.UpdateTagCategory;
public class UpdateTagCategoryCommandHandler : ICommandHandler<UpdateTagCategoryCommand> {
    private readonly ITagCategoryRepository _repository;
    public UpdateTagCategoryCommandHandler(ITagCategoryRepository repository) => _repository = repository;
    public async Task HandleAsync(UpdateTagCategoryCommand command, CancellationToken cancellationToken = default) {
        var entity = await _repository.GetByIdAsync(command.TagCategoryID, cancellationToken);
        if (entity == null) throw new KeyNotFoundException($"TagCategory with ID {command.TagCategoryID} not found.");
        entity.Name = command.Name; entity.Description = command.Description;

        entity.DisplayOrder = command.DisplayOrder; entity.IsActive = command.IsActive; entity.MarkModified();
        await _repository.UpdateAsync(entity, cancellationToken);
    }
}
