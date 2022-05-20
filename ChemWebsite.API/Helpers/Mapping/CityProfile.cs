using AutoMapper;
using ChemWebsite.Data;
using ChemWebsite.Data.Dto;

namespace ChemWebsite.API.Helpers.Mapping
{
    public class CityProfile : Profile
    {
        public CityProfile()
        {
            CreateMap<City, CityDto>().ReverseMap();
        }
    }
}
