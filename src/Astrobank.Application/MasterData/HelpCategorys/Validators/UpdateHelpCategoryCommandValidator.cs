using Astrobank.Application.MasterData.HelpCategorys.Commands.UpdateHelpCategory;
using FluentValidation;
namespace Astrobank.Application.MasterData.HelpCategorys.Validators;
public class UpdateHelpCategoryCommandValidator : AbstractValidator<UpdateHelpCategoryCommand> {
    public UpdateHelpCategoryCommandValidator() {
        RuleFor(x => x.HelpCategoryID).GreaterThan(0); RuleFor(x => x.Name).NotEmpty().MaximumLength(200); RuleFor(x => x.Description).MaximumLength(500);

    }
}
