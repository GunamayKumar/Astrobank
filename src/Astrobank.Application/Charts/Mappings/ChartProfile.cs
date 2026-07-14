using Astrobank.Application.Charts.DTOs;
using Astrobank.Domain.Charts;
using AutoMapper;
namespace Astrobank.Application.Charts.Mappings;
public class ChartProfile : Profile {
    public ChartProfile() {
        CreateMap<Chart, ChartDto>()
            .ForMember(d => d.UserName, opt => opt.MapFrom(src => src.User != null ? src.User.Name : string.Empty))
            .ForMember(d => d.ChartTypeName, opt => opt.MapFrom(src => src.ChartType != null ? src.ChartType.Name : string.Empty))
            .ForMember(d => d.ChartPermissionName, opt => opt.MapFrom(src => src.ChartPermission != null ? src.ChartPermission.Name : string.Empty))
            .ForMember(d => d.CountryName, opt => opt.MapFrom(src => src.Country != null ? src.Country.CountryName : string.Empty));
    }
}
