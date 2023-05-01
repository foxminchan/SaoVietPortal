using AutoMapper;

namespace Portal.Api.Mapping;

public class PaymentMethodProfile : Profile
{
    public PaymentMethodProfile()
        => CreateMap<Domain.Entities.PaymentMethod, Models.PaymentMethod>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ReverseMap();
}