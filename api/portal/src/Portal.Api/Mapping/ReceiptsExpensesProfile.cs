using AutoMapper;

namespace Portal.Api.Mapping;

public class ReceiptsExpensesProfile : Profile
{
    public ReceiptsExpensesProfile()
    {
        CreateMap<Domain.Entities.ReceiptsExpenses, Models.ReceiptsExpenses>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type))
            .ForMember(dest => dest.Amount,
opt => opt.MapFrom(src => src.Amount))
            .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.Date))
            .ForMember(dest => dest.Note, opt => opt.MapFrom(src => src.Note))
            .ForMember(dest => dest.BranchId, opt => opt.MapFrom(src => src.BranchId))
            .ReverseMap();
    }
}