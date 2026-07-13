using Astrobank.Application.MasterData.ChartTypes.Commands.UpdateChartType;
using FluentValidation;
namespace Astrobank.Application.MasterData.ChartTypes.Validators;
public class UpdateChartTypeCommandValidator : AbstractValidator<UpdateChartTypeCommand> {
    public UpdateChartTypeCommandValidator() {
        RuleFor(x => x.ChartTypeID).GreaterThan(0); RuleFor(x => x.Name).NotEmpty().MaximumLength(200); RuleFor(x => x.Description).MaximumLength(500);

    }
}
