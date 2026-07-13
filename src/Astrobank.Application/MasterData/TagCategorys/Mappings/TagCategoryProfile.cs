using Astrobank.Application.MasterData.TagCategorys.DTOs;
using Astrobank.Domain.MasterData;
using AutoMapper;
namespace Astrobank.Application.MasterData.TagCategorys.Mappings;
public class TagCategoryProfile : Profile {
    public TagCategoryProfile() { CreateMap<TagCategory, TagCategoryDto>(); }
}
