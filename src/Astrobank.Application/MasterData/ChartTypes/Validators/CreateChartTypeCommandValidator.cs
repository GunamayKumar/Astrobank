using Astrobank.Application.MasterData.ChartTypes.Commands.CreateChartType;
using FluentValidation;
namespace Astrobank.Application.MasterData.ChartTypes.Validators;
public class CreateChartTypeCommandValidator : AbstractValidator<CreateChartTypeCommand> {
    public CreateChartTypeCommandValidator() {
        RuleFor(x => x.Name).NotEmpty().MaximumLength(200); RuleFor(x => x.Description).MaximumLength(500);

    }
}
