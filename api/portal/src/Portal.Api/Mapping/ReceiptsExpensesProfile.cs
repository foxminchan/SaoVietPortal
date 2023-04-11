using AutoMapper;

namespace Portal.Api.Mapping;

public class ReceiptsExpensesProfile : Profile
{
    public ReceiptsExpensesProfile()
    {
        CreateMap<Domain.Entities.ReceiptsExpenses, Models.ReceiptsExpenses>()
            .ForMember(dest => dest.receiptExpenseId, opt => opt.MapFrom(src => src.receiptExpenseId))
            .ForMember(dest => dest.type, opt => opt.MapFrom(src => src.type))
            .ForMember(dest => dest.amount, 
opt => opt.MapFrom(src => src.amount))
            .ForMember(dest => dest.date, opt => opt.MapFrom(src => src.date))
            .ForMember(dest => dest.note, opt => opt.MapFrom(src => src.note))
            .ForMember(dest => dest.branchId, opt => opt.MapFrom(src => src.branchId))
            .ReverseMap();
    }
}