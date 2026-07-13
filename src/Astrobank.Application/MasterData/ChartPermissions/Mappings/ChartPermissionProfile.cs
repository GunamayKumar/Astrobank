using Astrobank.Application.MasterData.ChartPermissions.DTOs;
using Astrobank.Domain.MasterData;
using AutoMapper;
namespace Astrobank.Application.MasterData.ChartPermissions.Mappings;
public class ChartPermissionProfile : Profile {
    public ChartPermissionProfile() { CreateMap<ChartPermission, ChartPermissionDto>(); }
}
