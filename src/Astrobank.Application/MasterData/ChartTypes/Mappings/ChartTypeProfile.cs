using Astrobank.Application.MasterData.ChartTypes.DTOs;
using Astrobank.Domain.MasterData;
using AutoMapper;
namespace Astrobank.Application.MasterData.ChartTypes.Mappings;
public class ChartTypeProfile : Profile {
    public ChartTypeProfile() { CreateMap<ChartType, ChartTypeDto>(); }
}
