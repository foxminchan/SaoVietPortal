using AutoMapper;

namespace Portal.Api.Mapping;

public class UserProfile : Profile
{
    public UserProfile()
        => CreateMap<Domain.Entities.ApplicationUser, Models.User>()
            .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.ImageUrl))
            .ForMember(dest => dest.StudentId, opt => opt.MapFrom(src => src.StudentId))
            .ForMember(dest => dest.StaffId, opt => opt.MapFrom(src => src.StaffId))
            .ReverseMap();
}