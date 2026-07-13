using Astrobank.Application.Common.CQRS;
using Astrobank.Application.Users.Commands.LoginUser;
using Astrobank.Application.Users.Commands.RegisterUser;
using Astrobank.Application.Users.DTOs;
using Astrobank.Web.Models.Account;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Astrobank.Web.Controllers;

public class AccountController : Controller
{
    private readonly ICommandHandler<RegisterUserCommand, AuthenticationResultDto> _registerHandler;
    private readonly ICommandHandler<LoginUserCommand, AuthenticationResultDto> _loginHandler;

    public AccountController(
        ICommandHandler<RegisterUserCommand, AuthenticationResultDto> registerHandler,
        ICommandHandler<LoginUserCommand, AuthenticationResultDto> loginHandler)
    {
        _registerHandler = registerHandler;
        _loginHandler = loginHandler;
    }

    [HttpGet]
    [AllowAnonymous]
    public IActionResult Register()
    {
        if (User.Identity?.IsAuthenticated == true)
        {
            return RedirectToAction("Dashboard", "Home");
        }

        LoadValidationErrorsFromTempData();
        return View(new RegisterViewModel());
    }

    [HttpPost]
    [AllowAnonymous]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var command = new RegisterUserCommand
        {
            Name = model.Name,
            Email = model.Email,
            Username = model.Username,
            Password = model.Password,
            ConfirmPassword = model.ConfirmPassword,
            PhoneNo = model.PhoneNo,
            CountryID = model.CountryID,
            Gender = model.Gender,
            ReferralCode = model.ReferralCode
        };

        // If there are validation failures from FluentValidation, the global filter will catch them
        // and repopulate the ModelState, returning to this view automatically.
        var result = await _registerHandler.HandleAsync(command);

        if (result.Succeeded)
        {
            // Registration successful. Business rules say status is Pending.
            TempData["SuccessMessage"] = "Registration successful! Your account is pending activation.";
            return RedirectToAction(nameof(Login));
        }

        foreach (var error in result.Errors)
        {
            ModelState.AddModelError(string.Empty, error);
        }

        return View(model);
    }

    [HttpGet]
    [AllowAnonymous]
    public IActionResult Login(string? returnUrl = null)
    {
        if (User.Identity?.IsAuthenticated == true)
        {
            return RedirectToAction("Dashboard", "Home");
        }

        LoadValidationErrorsFromTempData();
        ViewData["ReturnUrl"] = returnUrl;
        return View(new LoginViewModel());
    }

    private void LoadValidationErrorsFromTempData()
    {
        if (TempData["ValidationErrors"] is string errorsJson)
        {
            var errors = System.Text.Json.JsonSerializer.Deserialize<IDictionary<string, string[]>>(errorsJson);
            if (errors != null)
            {
                foreach (var kvp in errors)
                {
                    foreach (var error in kvp.Value)
                    {
                        ModelState.AddModelError(kvp.Key, error);
                    }
                }
            }
        }
    }

    [HttpPost]
    [AllowAnonymous]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginViewModel model, string? returnUrl = null)
    {
        ViewData["ReturnUrl"] = returnUrl;

        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var command = new LoginUserCommand
        {
            Username = model.Username,
            Password = model.Password,
            RememberMe = model.RememberMe
        };

        var result = await _loginHandler.HandleAsync(command);

        if (result.Succeeded)
        {
            if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Dashboard", "Home");
        }

        foreach (var error in result.Errors)
        {
            ModelState.AddModelError(string.Empty, error);
        }

        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Logout()
    {
        // To properly implement logout while relying on CQRS and IdentityService:
        // We could create a LogoutCommand, or simply invoke HttpContext.SignOutAsync here.
        // Since SignInManager was injected and registered in Astrobank.Infrastructure,
        // calling HttpContext.SignOutAsync with IdentityConstants.ApplicationScheme is the standard way.
        await HttpContext.SignOutAsync(IdentityConstants.ApplicationScheme);

        return RedirectToAction("Index", "Home");
    }
}
