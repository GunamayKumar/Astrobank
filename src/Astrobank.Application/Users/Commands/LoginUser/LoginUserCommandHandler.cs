using Astrobank.Application.Common.CQRS;
using Astrobank.Application.Interfaces.Identity;
using Astrobank.Application.Users.DTOs;

namespace Astrobank.Application.Users.Commands.LoginUser;

public class LoginUserCommandHandler : ICommandHandler<LoginUserCommand, AuthenticationResultDto>
{
    private readonly IIdentityService _identityService;

    public LoginUserCommandHandler(IIdentityService identityService)
    {
        _identityService = identityService;
    }

    public async Task<AuthenticationResultDto> HandleAsync(LoginUserCommand command, CancellationToken cancellationToken = default)
    {
        var result = await _identityService.LoginAsync(command.Username, command.Password, command.RememberMe, cancellationToken);

        return new AuthenticationResultDto
        {
            Succeeded = result.Succeeded,
            Errors = result.Errors
        };
    }
}
