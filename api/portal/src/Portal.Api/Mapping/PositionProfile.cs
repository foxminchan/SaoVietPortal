using AutoMapper;

namespace Portal.Api.Mapping;

public class PositionProfile : Profile
{
    public PositionProfile()
    {
        CreateMap<Domain.Entities.Position, Models.Position>()
            .ForMember(dest => dest.positionId, opt => opt.MapFrom(src => src.positionId))
            .ForMember(dest => dest.positionName, opt => opt.MapFrom(src => src.positionName))
            .ReverseMap();
    }
}