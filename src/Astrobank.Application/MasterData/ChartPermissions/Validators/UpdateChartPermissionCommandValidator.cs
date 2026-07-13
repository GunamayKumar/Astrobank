using Astrobank.Application.MasterData.ChartPermissions.Commands.UpdateChartPermission;
using FluentValidation;
namespace Astrobank.Application.MasterData.ChartPermissions.Validators;
public class UpdateChartPermissionCommandValidator : AbstractValidator<UpdateChartPermissionCommand> {
    public UpdateChartPermissionCommandValidator() {
        RuleFor(x => x.ChartPermissionID).GreaterThan(0); RuleFor(x => x.Name).NotEmpty().MaximumLength(200); RuleFor(x => x.Description).MaximumLength(500);

    }
}
