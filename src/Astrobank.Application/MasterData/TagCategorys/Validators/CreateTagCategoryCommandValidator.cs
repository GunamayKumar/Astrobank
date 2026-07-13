using Astrobank.Application.MasterData.TagCategorys.Commands.CreateTagCategory;
using FluentValidation;
namespace Astrobank.Application.MasterData.TagCategorys.Validators;
public class CreateTagCategoryCommandValidator : AbstractValidator<CreateTagCategoryCommand> {
    public CreateTagCategoryCommandValidator() {
        RuleFor(x => x.Name).NotEmpty().MaximumLength(200); RuleFor(x => x.Description).MaximumLength(500);

    }
}
