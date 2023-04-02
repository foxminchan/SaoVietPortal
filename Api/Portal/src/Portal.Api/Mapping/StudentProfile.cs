using AutoMapper;

namespace Portal.Api.Mapping;

public class StudentProfile : Profile
{
    public StudentProfile()
    {
        CreateMap<Domain.Entities.Student, Models.Student>()
            .ForMember(dest => dest.studentId, opt => opt.MapFrom(src => src.studentId))
            .ForMember(dest => dest.fullname, opt => opt.MapFrom(src => src.fullname))
            .ForMember(dest => dest.gender, opt => opt.MapFrom(src => src.gender))
            .ForMember(dest => dest.address, opt => opt.MapFrom(src => src.address))
            .ForMember(dest => dest.dob, opt => opt.MapFrom(src => src.dob))
            .ForMember(dest => dest.pod, opt => opt.MapFrom(src => src.pod))
            .ForMember(dest => dest.occupation, opt => opt.MapFrom(src => src.occupation))
            .ForMember(dest => dest.socialNetwork, opt => opt.MapFrom(src => src.socialNetwork))
            .ReverseMap();
    }
}