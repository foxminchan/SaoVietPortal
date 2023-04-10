using AutoMapper;

namespace Portal.Api.Mapping;

public class CourseProfile : Profile
{
    public CourseProfile()
    {
        CreateMap<Domain.Entities.Course, Models.Course>()
            .ForMember(dest => dest.courseId, opt => opt.MapFrom(src => src.courseId))
            .ForMember(dest => dest.courseName, opt => opt.MapFrom(src => src.courseName))
            .ForMember(dest => dest.description, opt => opt.MapFrom(src => src.description))
            .ReverseMap();
    }
}