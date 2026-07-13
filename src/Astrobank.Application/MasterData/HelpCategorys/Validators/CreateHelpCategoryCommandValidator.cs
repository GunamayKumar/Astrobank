using Astrobank.Application.MasterData.HelpCategorys.Commands.CreateHelpCategory;
using FluentValidation;
namespace Astrobank.Application.MasterData.HelpCategorys.Validators;
public class CreateHelpCategoryCommandValidator : AbstractValidator<CreateHelpCategoryCommand> {
    public CreateHelpCategoryCommandValidator() {
        RuleFor(x => x.Name).NotEmpty().MaximumLength(200); RuleFor(x => x.Description).MaximumLength(500);

    }
}
