using AutoMapper;
using ChemWebsite.Data;
using ChemWebsite.Data.Dto;
using ChemWebsite.MediatR.CommandAndQuery;

namespace ChemWebsite.API.Helpers.Mapping
{
    public class ChemicalProfile : Profile
    {
        public ChemicalProfile()
        {
            CreateMap<ChemicalIndustryDto, ChemicalIndustry>().ReverseMap();
            CreateMap<ChemicalCategoryDto, ChemicalCategory>().ReverseMap();
            CreateMap<Chemical, ChemicalDto>().ReverseMap();
            CreateMap<AddChemicalCommand, Chemical>();
            CreateMap<UpdateChemicalCommand, Chemical>();
         
        }
    }
}
