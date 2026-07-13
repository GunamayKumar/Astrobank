using Astrobank.Application.Common.CQRS;
using Astrobank.Application.Interfaces.Repositories;
using Astrobank.Domain.MasterData;
namespace Astrobank.Application.MasterData.ChartTypes.Commands.CreateChartType;
public class CreateChartTypeCommandHandler : ICommandHandler<CreateChartTypeCommand> {
    private readonly IChartTypeRepository _repository;
    public CreateChartTypeCommandHandler(IChartTypeRepository repository) => _repository = repository;
    public async Task HandleAsync(CreateChartTypeCommand command, CancellationToken cancellationToken = default) {
        var entity = new ChartType {
            Name = command.Name, Description = command.Description,

            DisplayOrder = command.DisplayOrder, IsActive = command.IsActive
        };
        entity.SetCreatedOn(DateTime.UtcNow);
        await _repository.AddAsync(entity, cancellationToken);
    }
}
