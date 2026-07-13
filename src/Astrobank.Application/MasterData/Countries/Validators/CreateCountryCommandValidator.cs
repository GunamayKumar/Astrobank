using Astrobank.Application.MasterData.Countries.Commands.CreateCountry;
using FluentValidation;
namespace Astrobank.Application.MasterData.Countries.Validators;
public class CreateCountryCommandValidator : AbstractValidator<CreateCountryCommand> {
    public CreateCountryCommandValidator() {
        RuleFor(x => x.CountryName).NotEmpty().MaximumLength(200);
        RuleFor(x => x.ISOCode2).NotEmpty().Length(2); RuleFor(x => x.ISOCode3).NotEmpty().Length(3); RuleFor(x => x.PhoneCode).MaximumLength(10);
    }
}
