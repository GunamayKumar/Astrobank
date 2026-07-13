using Astrobank.Domain.Users;
using AutoMapper;

namespace Astrobank.Application.Users.Mappings;

public class RegisterUserProfile : Profile
{
    public RegisterUserProfile()
    {
        // Example mapping if we need to map Command -> User directly without IdentityService abstraction.
        // Given we are using IIdentityService, we might not strictly need this AutoMapper profile for the core logic,
        // but it's good practice to have it set up as requested.
        CreateMap<Commands.RegisterUser.RegisterUserCommand, User>()
            .ForMember(dest => dest.PasswordHash, opt => opt.Ignore())
            .ForMember(dest => dest.RoleID, opt => opt.Ignore())
            .ForMember(dest => dest.Status, opt => opt.Ignore())
            .ForMember(dest => dest.UserID, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedOn, opt => opt.Ignore())
            .ForMember(dest => dest.ModifiedOn, opt => opt.Ignore())
            .ForMember(dest => dest.IsDeleted, opt => opt.Ignore())
            .ForMember(dest => dest.IsVerifiedAstrologer, opt => opt.Ignore())
            .ForMember(dest => dest.IPAddress, opt => opt.Ignore());
    }
}
