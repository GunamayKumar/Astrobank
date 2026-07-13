using Astrobank.Application.MasterData.Countries.Commands.UpdateCountry;
using FluentValidation;
namespace Astrobank.Application.MasterData.Countries.Validators;
public class UpdateCountryCommandValidator : AbstractValidator<UpdateCountryCommand> {
    public UpdateCountryCommandValidator() {
        RuleFor(x => x.CountryID).GreaterThan(0); RuleFor(x => x.CountryName).NotEmpty().MaximumLength(200);
        RuleFor(x => x.ISOCode2).NotEmpty().Length(2); RuleFor(x => x.ISOCode3).NotEmpty().Length(3); RuleFor(x => x.PhoneCode).MaximumLength(10);
    }
}
