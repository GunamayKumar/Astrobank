using Astrobank.Application.Common.CQRS;
using Astrobank.Application.Interfaces.Repositories;
using System.Collections.Generic;
namespace Astrobank.Application.MasterData.ChartTypes.Commands.UpdateChartType;
public class UpdateChartTypeCommandHandler : ICommandHandler<UpdateChartTypeCommand> {
    private readonly IChartTypeRepository _repository;
    public UpdateChartTypeCommandHandler(IChartTypeRepository repository) => _repository = repository;
    public async Task HandleAsync(UpdateChartTypeCommand command, CancellationToken cancellationToken = default) {
        var entity = await _repository.GetByIdAsync(command.ChartTypeID, cancellationToken);
        if (entity == null) throw new KeyNotFoundException($"ChartType with ID {command.ChartTypeID} not found.");
        entity.Name = command.Name; entity.Description = command.Description;

        entity.DisplayOrder = command.DisplayOrder; entity.IsActive = command.IsActive; entity.MarkModified();
        await _repository.UpdateAsync(entity, cancellationToken);
    }
}
