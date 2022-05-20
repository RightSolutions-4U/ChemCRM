using AutoMapper;
using ChemWebsite.Data;
using ChemWebsite.Data.Dto;
using ChemWebsite.MediatR.Command;

namespace ChemWebsite.API.Helpers.Mapping
{
    public class IndustryProfile : Profile
    {
        public IndustryProfile()
        {
            CreateMap<AddIndustryCommand, Industry>();
            CreateMap<UpdateIndustryCommand, Industry>();
            CreateMap<IndustryDto, Industry>()
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description == null ? string.Empty : src.Description)).ReverseMap();
        }
    }
}
