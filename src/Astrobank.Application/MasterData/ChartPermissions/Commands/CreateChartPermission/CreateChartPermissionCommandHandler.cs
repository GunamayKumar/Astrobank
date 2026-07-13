using Astrobank.Application.Common.CQRS;
using Astrobank.Application.Interfaces.Repositories;
using Astrobank.Domain.MasterData;
namespace Astrobank.Application.MasterData.ChartPermissions.Commands.CreateChartPermission;
public class CreateChartPermissionCommandHandler : ICommandHandler<CreateChartPermissionCommand> {
    private readonly IChartPermissionRepository _repository;
    public CreateChartPermissionCommandHandler(IChartPermissionRepository repository) => _repository = repository;
    public async Task HandleAsync(CreateChartPermissionCommand command, CancellationToken cancellationToken = default) {
        var entity = new ChartPermission {
            Name = command.Name, Description = command.Description,

            DisplayOrder = command.DisplayOrder, IsActive = command.IsActive
        };
        entity.SetCreatedOn(DateTime.UtcNow);
        await _repository.AddAsync(entity, cancellationToken);
    }
}
