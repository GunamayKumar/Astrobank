using Astrobank.Application.MasterData.EventTypes.Commands.UpdateEventType;
using FluentValidation;
namespace Astrobank.Application.MasterData.EventTypes.Validators;
public class UpdateEventTypeCommandValidator : AbstractValidator<UpdateEventTypeCommand> {
    public UpdateEventTypeCommandValidator() {
        RuleFor(x => x.EventTypeID).GreaterThan(0); RuleFor(x => x.Name).NotEmpty().MaximumLength(200); RuleFor(x => x.Description).MaximumLength(500);
        RuleFor(x => x.Category).MaximumLength(100);
    }
}
