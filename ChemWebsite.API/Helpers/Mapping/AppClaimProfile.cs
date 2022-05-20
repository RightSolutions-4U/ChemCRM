using AutoMapper;
using ChemWebsite.Data.Dto;
using System.Security.Claims;

namespace ChemWebsite.API.Helpers.Mapping
{
    public class AppClaimProfile : Profile
    {
        public AppClaimProfile()
        {
            CreateMap<AppClaimDto, Claim>()
               .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.ClaimType))
               .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.ClaimValue))
               .ReverseMap();

          
        }
    }
}