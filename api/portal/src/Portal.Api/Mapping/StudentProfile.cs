using AutoMapper;
using Portal.Api.Models;
using Portal.Domain.Entities;

namespace Portal.Api.Mapping;

public class StudentProfile : Profile
{
    public StudentProfile()
        => CreateMap<Student, StudentResponse>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Fullname, opt => opt.MapFrom(src => src.Fullname))
            .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.Gender))
            .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address))
            .ForMember(dest => dest.Dob, opt => opt.MapFrom(src => src.Dob))
            .ForMember(dest => dest.Pod, opt => opt.MapFrom(src => src.Pod))
            .ForMember(dest => dest.Occupation, opt => opt.MapFrom(src => src.Occupation))
            .ForMember(dest => dest.SocialNetwork, opt => opt.MapFrom(src => src.SocialNetwork))
            .ReverseMap();
}