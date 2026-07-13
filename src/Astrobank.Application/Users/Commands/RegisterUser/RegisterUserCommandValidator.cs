using FluentValidation;

namespace Astrobank.Application.Users.Commands.RegisterUser;

public class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
{
    private readonly Interfaces.Repositories.IUserRepository _userRepository;

    public RegisterUserCommandValidator(Interfaces.Repositories.IUserRepository userRepository)
    {
        _userRepository = userRepository;

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required.")
            .Length(2, 100).WithMessage("Name must be between 2 and 100 characters.");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("A valid email is required.")
            .MustAsync(BeUniqueEmail).WithMessage("The specified email already exists.");

        RuleFor(x => x.Username)
            .NotEmpty().WithMessage("Username is required.")
            .Length(4, 30).WithMessage("Username must be between 4 and 30 characters.")
            .Matches("^[a-zA-Z][a-zA-Z0-9_]*$").WithMessage("Username must start with a letter and contain only letters, numbers, and underscores.")
            .MustAsync(BeUniqueUsername).WithMessage("The specified username already exists.");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required.")
            .MinimumLength(8).WithMessage("Password must be at least 8 characters.")
            .Matches("[A-Z]").WithMessage("Password must contain at least one uppercase letter.")
            .Matches("[a-z]").WithMessage("Password must contain at least one lowercase letter.")
            .Matches("[0-9]").WithMessage("Password must contain at least one number.")
            .Matches("[^a-zA-Z0-9]").WithMessage("Password must contain at least one special character.");

        RuleFor(x => x.ConfirmPassword)
            .Equal(x => x.Password).WithMessage("Passwords do not match.");

        RuleFor(x => x.PhoneNo)
            .Matches("^[0-9]*$").WithMessage("Phone number must contain only digits.")
            .Length(8, 20).When(x => !string.IsNullOrEmpty(x.PhoneNo)).WithMessage("Phone number must be between 8 and 20 characters.");

        RuleFor(x => x.CountryID)
            .GreaterThan(0).WithMessage("Country is required.");

        RuleFor(x => x.Gender)
            .IsInEnum().WithMessage("A valid gender must be selected.");

        RuleFor(x => x.ReferralCode)
            .MaximumLength(50).WithMessage("Referral code cannot exceed 50 characters.");
    }

    private async Task<bool> BeUniqueEmail(string email, CancellationToken cancellationToken)
    {
        var existingUser = await _userRepository.GetByEmailAsync(email, cancellationToken);
        return existingUser == null;
    }

    private async Task<bool> BeUniqueUsername(string username, CancellationToken cancellationToken)
    {
        var existingUser = await _userRepository.GetByUsernameAsync(username, cancellationToken);
        return existingUser == null;
    }
}
