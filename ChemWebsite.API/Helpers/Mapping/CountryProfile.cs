using AutoMapper;
using ChemWebsite.Data;
using ChemWebsite.Data.Dto;

namespace ChemWebsite.API.Helpers.Mapping
{
    public class CountryProfile : Profile
    {
        public CountryProfile()
        {
            CreateMap<Country, CountryDto>().ReverseMap();
        }
    }
}
