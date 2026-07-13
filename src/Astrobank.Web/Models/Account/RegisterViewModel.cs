using Astrobank.Domain.Users.Enums;
using System.ComponentModel.DataAnnotations;

namespace Astrobank.Web.Models.Account;

public class RegisterViewModel
{
    [Required]
    [Display(Name = "Full Name")]
    public string Name { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    [Display(Name = "Email Address")]
    public string Email { get; set; } = string.Empty;

    [Required]
    [Display(Name = "Username")]
    public string Username { get; set; } = string.Empty;

    [Required]
    [DataType(DataType.Password)]
    [Display(Name = "Password")]
    public string Password { get; set; } = string.Empty;

    [Required]
    [DataType(DataType.Password)]
    [Display(Name = "Confirm Password")]
    public string ConfirmPassword { get; set; } = string.Empty;

    [Display(Name = "Phone Number (Optional)")]
    public string? PhoneNo { get; set; }

    [Required]
    [Display(Name = "Country")]
    public int CountryID { get; set; }

    [Required]
    [Display(Name = "Gender")]
    public Gender Gender { get; set; }

    [Display(Name = "Referral Code (Optional)")]
    public string? ReferralCode { get; set; }
}
