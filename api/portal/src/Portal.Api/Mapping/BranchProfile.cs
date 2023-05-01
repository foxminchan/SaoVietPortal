using AutoMapper;

namespace Portal.Api.Mapping;

public class BranchProfile : Profile
{
    public BranchProfile()
        => CreateMap<Domain.Entities.Branch, Models.Branch>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address))
            .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.Phone))
            .ReverseMap();
}