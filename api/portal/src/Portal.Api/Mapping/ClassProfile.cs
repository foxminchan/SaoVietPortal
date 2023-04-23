using AutoMapper;

namespace Portal.Api.Mapping;

public class ClassProfile : Profile
{
    public ClassProfile()
    {
        CreateMap<Domain.Entities.Class, Models.Class>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => src.StartDate))
            .ForMember(dest => dest.EndDate, opt => opt.MapFrom(src => src.EndDate))
            .ForMember(dest => dest.Fee, opt => opt.MapFrom(src => src.Fee))
            .ForMember(dest => dest.CourseId, opt => opt.MapFrom(src => src.CourseId))
            .ForMember(dest => dest.BranchId, opt => opt.MapFrom(src => src.BranchId))
            .ReverseMap();
    }
}