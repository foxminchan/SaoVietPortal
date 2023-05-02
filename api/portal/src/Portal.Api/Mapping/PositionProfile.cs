using AutoMapper;
using Portal.Api.Models;
using Portal.Domain.Entities;

namespace Portal.Api.Mapping;

public class PositionProfile : Profile
{
    public PositionProfile()
        => CreateMap<Position, PositionResponse>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ReverseMap();
}