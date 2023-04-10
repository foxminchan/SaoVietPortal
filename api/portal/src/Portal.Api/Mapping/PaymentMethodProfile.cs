using AutoMapper;

namespace Portal.Api.Mapping;

public class PaymentMethodProfile : Profile
{
    public PaymentMethodProfile()
    {
        CreateMap<Domain.Entities.PaymentMethod, Models.PaymentMethod>()
            .ForMember(dest => dest.paymentMethodId, opt => opt.MapFrom(src => src.paymentMethodId))
            .ForMember(dest => dest.paymentMethodName, opt => opt.MapFrom(src => src.paymentMethodName))
            .ReverseMap();
    }
}