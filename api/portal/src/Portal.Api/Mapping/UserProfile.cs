using AutoMapper;
using Portal.Api.Models;
using Portal.Domain.Entities;

namespace Portal.Api.Mapping;

public class UserProfile : Profile
{
    public UserProfile()
        => CreateMap<ApplicationUser, UserResponse>()
            .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.ImageUrl))
            .ForMember(dest => dest.StudentId, opt => opt.MapFrom(src => src.StudentId))
            .ForMember(dest => dest.StaffId, opt => opt.MapFrom(src => src.StaffId))
            .ReverseMap();
}