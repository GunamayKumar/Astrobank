using Astrobank.Application.MasterData.EventTypes.Commands.CreateEventType;
using FluentValidation;
namespace Astrobank.Application.MasterData.EventTypes.Validators;
public class CreateEventTypeCommandValidator : AbstractValidator<CreateEventTypeCommand> {
    public CreateEventTypeCommandValidator() {
        RuleFor(x => x.Name).NotEmpty().MaximumLength(200); RuleFor(x => x.Description).MaximumLength(500);
        RuleFor(x => x.Category).MaximumLength(100);
    }
}
