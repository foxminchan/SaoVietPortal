using AutoMapper;

namespace Portal.Api.Mapping;

public class BranchProfile : Profile
{
    public BranchProfile()
    {
        CreateMap<Domain.Entities.Branch, Models.Branch>()
            .ForMember(dest => dest.branchId, opt => opt.MapFrom(src => src.branchId))
            .ForMember(dest => dest.branchName, opt => opt.MapFrom(src => src.branchName))
            .ForMember(dest => dest.address, opt => opt.MapFrom(src => src.address))
            .ForMember(dest => dest.phone, opt => opt.MapFrom(src => src.phone))
            .ReverseMap();
    }
}