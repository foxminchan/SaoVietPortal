using AutoMapper;

namespace Portal.Api.Mapping;

public class StaffProfile : Profile
{
    public StaffProfile()
    {
        CreateMap<Domain.Entities.Staff, Models.Staff>()
            .ForMember(dest => dest.staffId, opt => opt.MapFrom(src => src.staffId))
            .ForMember(dest => dest.fullname, opt => opt.MapFrom(src => src.fullname))
            .ForMember(dest => dest.dob, opt => opt.MapFrom(src => src.dob))
            .ForMember(dest => dest.dsw, opt => opt.MapFrom(src => src.dsw))
            .ForMember(dest => dest.address, opt => opt.MapFrom(src => src.address))
            .ForMember(dest => dest.positionId, opt => opt.MapFrom(src => src.positionId))
            .ForMember(dest => dest.branchId, opt => opt.MapFrom(src => src.branchId))
            .ForMember(dest => dest.managerId, opt => opt.MapFrom(src => src.managerId))
            .ReverseMap();
    }
}