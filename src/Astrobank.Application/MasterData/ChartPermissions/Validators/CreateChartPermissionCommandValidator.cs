using Astrobank.Application.MasterData.ChartPermissions.Commands.CreateChartPermission;
using FluentValidation;
namespace Astrobank.Application.MasterData.ChartPermissions.Validators;
public class CreateChartPermissionCommandValidator : AbstractValidator<CreateChartPermissionCommand> {
    public CreateChartPermissionCommandValidator() {
        RuleFor(x => x.Name).NotEmpty().MaximumLength(200); RuleFor(x => x.Description).MaximumLength(500);

    }
}
