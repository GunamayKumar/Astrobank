using Astrobank.Application.Charts.Commands.UpdateChart;
using FluentValidation;
namespace Astrobank.Application.Charts.Validators;
public class UpdateChartCommandValidator : AbstractValidator<UpdateChartCommand> {
    public UpdateChartCommandValidator() {
        RuleFor(x => x.ChartID).GreaterThan(0); RuleFor(x => x.UserID).GreaterThan(0); RuleFor(x => x.ChartTypeID).GreaterThan(0); RuleFor(x => x.ChartPermissionID).GreaterThan(0); RuleFor(x => x.CountryID).GreaterThan(0);
        RuleFor(x => x.Name).NotEmpty().MaximumLength(256); RuleFor(x => x.Alias).MaximumLength(256); RuleFor(x => x.BirthPlace).NotEmpty().MaximumLength(500); RuleFor(x => x.Timezone).NotEmpty().MaximumLength(100); RuleFor(x => x.Description).MaximumLength(4000);
    }
}
