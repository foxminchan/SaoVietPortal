using AutoMapper;

namespace Portal.Api.Mapping;

public class CourseRegistrationProfile : Profile
{
    public CourseRegistrationProfile()
        => CreateMap<Domain.Entities.CourseRegistration, Models.CourseRegistration>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
                .ForMember(dest => dest.RegisterDate, opt => opt.MapFrom(src => src.RegisterDate))
            .ForMember(dest => dest.AppointmentDate, opt => opt.MapFrom(src => src.AppointmentDate))
            .ForMember(dest => dest.Fee, opt => opt.MapFrom(src => src.Fee))
            .ForMember(dest => dest.DiscountAmount, opt => opt.MapFrom(src => src.DiscountAmount))
            .ForMember(dest => dest.PaymentMethodId, opt => opt.MapFrom(src => src.PaymentMethodId))
            .ForMember(dest => dest.StudentId, opt => opt.MapFrom(src => src.StudentId))
            .ForMember(dest => dest.ClassId, opt => opt.MapFrom(src => src.ClassId));
}