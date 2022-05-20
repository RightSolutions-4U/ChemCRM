using AutoMapper;
using ChemWebsite.Data.Dto;

namespace ChemWebsite.API.Helpers.Mapping
{
    public class NLogProfile : Profile
    {
        public NLogProfile()
        {
            CreateMap<Data.NLog, NLogDto>().ReverseMap();
        }
    }
}
