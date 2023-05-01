using AutoMapper;

namespace Portal.Api.Mapping;

public class CourseProfile : Profile
{
    public CourseProfile()
        => CreateMap<Domain.Entities.Course, Models.Course>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ReverseMap();
}