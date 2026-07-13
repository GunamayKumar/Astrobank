using Astrobank.Application.Common.CQRS;
using Astrobank.Application.Interfaces.Repositories;
using System.Collections.Generic;
namespace Astrobank.Application.MasterData.ChartPermissions.Commands.UpdateChartPermission;
public class UpdateChartPermissionCommandHandler : ICommandHandler<UpdateChartPermissionCommand> {
    private readonly IChartPermissionRepository _repository;
    public UpdateChartPermissionCommandHandler(IChartPermissionRepository repository) => _repository = repository;
    public async Task HandleAsync(UpdateChartPermissionCommand command, CancellationToken cancellationToken = default) {
        var entity = await _repository.GetByIdAsync(command.ChartPermissionID, cancellationToken);
        if (entity == null) throw new KeyNotFoundException($"ChartPermission with ID {command.ChartPermissionID} not found.");
        entity.Name = command.Name; entity.Description = command.Description;

        entity.DisplayOrder = command.DisplayOrder; entity.IsActive = command.IsActive; entity.MarkModified();
        await _repository.UpdateAsync(entity, cancellationToken);
    }
}
