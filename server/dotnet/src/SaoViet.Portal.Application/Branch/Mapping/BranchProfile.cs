using AutoMapper;
using SaoViet.Portal.Application.Branch.DTOs;

namespace SaoViet.Portal.Application.Branch.Mapping;

public class BranchProfile : Profile
{
    public BranchProfile()
        => CreateMap<Domain.Entities.Branch, BranchDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address))
            .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.Phone))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
            .ReverseMap();
}