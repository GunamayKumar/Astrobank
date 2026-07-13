using Astrobank.Application.Common.CQRS;
using Astrobank.Application.Interfaces.Identity;
using Astrobank.Application.Interfaces.Repositories;
using Astrobank.Application.Users.DTOs;
using Astrobank.Domain.Users;

namespace Astrobank.Application.Users.Commands.RegisterUser;

public class RegisterUserCommandHandler : ICommandHandler<RegisterUserCommand, AuthenticationResultDto>
{
    private readonly IIdentityService _identityService;
    private readonly IRoleRepository _roleRepository;
    private readonly IUserRepository _userRepository;
    private readonly AutoMapper.IMapper _mapper;

    public RegisterUserCommandHandler(
        IIdentityService identityService,
        IRoleRepository roleRepository,
        IUserRepository userRepository,
        AutoMapper.IMapper mapper)
    {
        _identityService = identityService;
        _roleRepository = roleRepository;
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<AuthenticationResultDto> HandleAsync(RegisterUserCommand command, CancellationToken cancellationToken = default)
    {
        // Users should be given the "StandardUser" role by default.
        var defaultRole = await _roleRepository.GetByNameAsync("StandardUser", cancellationToken);
        if (defaultRole == null)
        {
            throw new InvalidOperationException("Default role 'StandardUser' not found in the database.");
        }

        var user = _mapper.Map<User>(command);

        // Setup initial default business rules
        user.AssignRole(defaultRole.RoleID);
        user.SetRegistrationDetails(command.PhoneNo, command.Gender, command.CountryID);

        var result = await _identityService.RegisterUserAsync(
            user.Email,
            user.Username,
            command.Password,
            user.RoleID,
            user.Name,
            cancellationToken);

        // Since IdentityService creates its own User right now (due to the abstraction),
        // we need to fetch the created user and update its additional mapped details.
        if (result.Succeeded)
        {
            var createdUser = await _userRepository.GetByUsernameAsync(user.Username, cancellationToken);
            if (createdUser != null)
            {
                createdUser.SetRegistrationDetails(user.PhoneNo, user.Gender, user.CountryID ?? 0);
                if (user.ReferralCode != null)
                {
                    // Reflection fallback just for ReferralCode if we didn't add a domain method, or we can add it.
                    typeof(User).GetProperty(nameof(User.ReferralCode))!.SetValue(createdUser, user.ReferralCode);
                }

                await _userRepository.UpdateAsync(createdUser, cancellationToken);
            }
        }

        return new AuthenticationResultDto
        {
            Succeeded = result.Succeeded,
            Errors = result.Errors
        };
    }
}
