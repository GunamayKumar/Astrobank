using Astrobank.Application.MasterData.HelpCategorys.DTOs;
using Astrobank.Domain.MasterData;
using AutoMapper;
namespace Astrobank.Application.MasterData.HelpCategorys.Mappings;
public class HelpCategoryProfile : Profile {
    public HelpCategoryProfile() { CreateMap<HelpCategory, HelpCategoryDto>(); }
}
