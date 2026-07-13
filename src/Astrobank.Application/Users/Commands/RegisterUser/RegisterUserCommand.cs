using Astrobank.Domain.Users.Enums;

namespace Astrobank.Application.Users.Commands.RegisterUser;

public class RegisterUserCommand
{
    public required string Name { get; set; }
    public required string Email { get; set; }
    public required string Username { get; set; }
    public required string Password { get; set; }
    public required string ConfirmPassword { get; set; }
    public string? PhoneNo { get; set; }
    public required int CountryID { get; set; }
    public required Gender Gender { get; set; }
    public string? ReferralCode { get; set; }
}
