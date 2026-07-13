using Astrobank.Application.MasterData.Countries.DTOs;
using Astrobank.Domain.Users;
using AutoMapper;
namespace Astrobank.Application.MasterData.Countries.Mappings;
public class CountryProfile : Profile {
    public CountryProfile() { CreateMap<Country, CountryDto>(); }
}
