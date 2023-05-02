using AutoMapper;
using Portal.Api.Models;
using Portal.Domain.Entities;

namespace Portal.Api.Mapping;

public class StudentProgressProfile : Profile
{
    public StudentProgressProfile()
        => CreateMap<StudentProgress, StudentProgressResponse>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.LessonName, opt => opt.MapFrom(src => src.LessonName))
            .ForMember(dest => dest.LessonContent, opt => opt.MapFrom(src => src.LessonContent))
            .ForMember(dest => dest.LessonDate, opt => opt.MapFrom(src => src.LessonDate))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
            .ForMember(dest => dest.LessonRating, opt => opt.MapFrom(src => src.LessonRating))
            .ForMember(dest => dest.StaffId, opt => opt.MapFrom(src => src.StaffId))
            .ForMember(dest => dest.StudentId, opt => opt.MapFrom(src => src.StudentId))
            .ForMember(dest => dest.ClassId, opt => opt.MapFrom(src => src.ClassId))
            .ReverseMap();
}