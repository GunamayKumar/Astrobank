using Astrobank.Application.Common.CQRS;
using Astrobank.Application.Interfaces.Repositories;
using Astrobank.Domain.MasterData;
namespace Astrobank.Application.MasterData.TagCategorys.Commands.CreateTagCategory;
public class CreateTagCategoryCommandHandler : ICommandHandler<CreateTagCategoryCommand> {
    private readonly ITagCategoryRepository _repository;
    public CreateTagCategoryCommandHandler(ITagCategoryRepository repository) => _repository = repository;
    public async Task HandleAsync(CreateTagCategoryCommand command, CancellationToken cancellationToken = default) {
        var entity = new TagCategory {
            Name = command.Name, Description = command.Description,

            DisplayOrder = command.DisplayOrder, IsActive = command.IsActive
        };
        entity.SetCreatedOn(DateTime.UtcNow);
        await _repository.AddAsync(entity, cancellationToken);
    }
}
