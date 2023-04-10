using AutoMapper;

namespace Portal.Api.Mapping;

public class ClassProfile : Profile
{
    public ClassProfile()
    {
        CreateMap<Domain.Entities.Class, Models.Class>()
            .ForMember(dest => dest.classId, opt => opt.MapFrom(src => src.classId))
            .ForMember(dest => dest.startDate, opt => opt.MapFrom(src => src.startDate))
            .ForMember(dest => dest.endDate, opt => opt.MapFrom(src => src.endDate))
            .ForMember(dest => dest.fee, opt => opt.MapFrom(src => src.fee))
            .ForMember(dest => dest.courseId, opt => opt.MapFrom(src => src.courseId))
            .ForMember(dest => dest.branchId, opt => opt.MapFrom(src => src.branchId))
            .ReverseMap();
    }
}