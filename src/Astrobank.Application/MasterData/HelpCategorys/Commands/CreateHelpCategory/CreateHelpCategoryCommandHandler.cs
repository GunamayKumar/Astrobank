using Astrobank.Application.Common.CQRS;
using Astrobank.Application.Interfaces.Repositories;
using Astrobank.Domain.MasterData;
namespace Astrobank.Application.MasterData.HelpCategorys.Commands.CreateHelpCategory;
public class CreateHelpCategoryCommandHandler : ICommandHandler<CreateHelpCategoryCommand> {
    private readonly IHelpCategoryRepository _repository;
    public CreateHelpCategoryCommandHandler(IHelpCategoryRepository repository) => _repository = repository;
    public async Task HandleAsync(CreateHelpCategoryCommand command, CancellationToken cancellationToken = default) {
        var entity = new HelpCategory {
            Name = command.Name, Description = command.Description,

            DisplayOrder = command.DisplayOrder, IsActive = command.IsActive
        };
        entity.SetCreatedOn(DateTime.UtcNow);
        await _repository.AddAsync(entity, cancellationToken);
    }
}
