using Astrobank.Application.MasterData.TagCategorys.Commands.UpdateTagCategory;
using FluentValidation;
namespace Astrobank.Application.MasterData.TagCategorys.Validators;
public class UpdateTagCategoryCommandValidator : AbstractValidator<UpdateTagCategoryCommand> {
    public UpdateTagCategoryCommandValidator() {
        RuleFor(x => x.TagCategoryID).GreaterThan(0); RuleFor(x => x.Name).NotEmpty().MaximumLength(200); RuleFor(x => x.Description).MaximumLength(500);

    }
}
