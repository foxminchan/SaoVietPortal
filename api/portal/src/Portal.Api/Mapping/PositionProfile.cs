using AutoMapper;

namespace Portal.Api.Mapping;

public class PositionProfile : Profile
{
    public PositionProfile()
        => CreateMap<Domain.Entities.Position, Models.Position>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ReverseMap();
}