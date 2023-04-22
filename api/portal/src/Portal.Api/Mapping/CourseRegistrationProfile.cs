using AutoMapper;

namespace Portal.Api.Mapping;

public class CourseRegistrationProfile : Profile
{
    public CourseRegistrationProfile()
    {
        CreateMap<Domain.Entities.CourseRegistration, Models.CourseRegistration>()
            .ForMember(dest => dest.courseRegistrationId, opt => opt.MapFrom(src => src.courseRegistrationId))
            .ForMember(dest => dest.status, opt => opt.MapFrom(src => src.status))
                .ForMember(dest => dest.registerDate, opt => opt.MapFrom(src => src.registerDate))
            .ForMember(dest => dest.appointmentDate, opt => opt.MapFrom(src => src.appointmentDate))
            .ForMember(dest => dest.registerFee, opt => opt.MapFrom(src => src.registerFee))
            .ForMember(dest => dest.discountAmount, opt => opt.MapFrom(src => src.discountAmount))
            .ForMember(dest => dest.paymentMethodId, opt => opt.MapFrom(src => src.paymentMethodId))
            .ForMember(dest => dest.studentId, opt => opt.MapFrom(src => src.studentId))
            .ForMember(dest => dest.classId, opt => opt.MapFrom(src => src.classId));
    }
}