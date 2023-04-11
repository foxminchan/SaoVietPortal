using AutoMapper;

namespace Portal.Api.Mapping;

public class StudentProgressProfile : Profile
{
    public StudentProgressProfile()
    {
        CreateMap<Domain.Entities.StudentProgress, Models.StudentProgress>()
            .ForMember(dest => dest.progressId, opt => opt.MapFrom(src => src.progressId))
            .ForMember(dest => dest.lessonName, opt => opt.MapFrom(src => src.lessonName))
            .ForMember(dest => dest.lessonContent, opt => opt.MapFrom(src => src.lessonContent))
            .ForMember(dest => dest.lessonDate, opt => opt.MapFrom(src => src.lessonDate))
            .ForMember(dest => dest.progressStatus, opt => opt.MapFrom(src => src.progressStatus))
            .ForMember(dest => dest.lessonRating, opt => opt.MapFrom(src => src.lessonRating))
            .ForMember(dest => dest.staffId, opt => opt.MapFrom(src => src.staffId))
            .ForMember(dest => dest.studentId, opt => opt.MapFrom(src => src.studentId))
            .ForMember(dest => dest.classId, opt => opt.MapFrom(src => src.classId))
            .ReverseMap();
    }
}
