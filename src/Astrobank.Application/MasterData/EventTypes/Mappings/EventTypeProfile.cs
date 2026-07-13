using Astrobank.Application.MasterData.EventTypes.DTOs;
using Astrobank.Domain.MasterData;
using AutoMapper;
namespace Astrobank.Application.MasterData.EventTypes.Mappings;
public class EventTypeProfile : Profile {
    public EventTypeProfile() { CreateMap<EventType, EventTypeDto>(); }
}
