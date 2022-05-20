using AutoMapper;
using ChemWebsite.Data;
using ChemWebsite.Data.Dto;
using ChemWebsite.MediatR.CommandAndQuery;

namespace ChemWebsite.API.Helpers.Mapping
{
    public class PageActionProfile : Profile
    {
        public PageActionProfile()
        {
            CreateMap<PageAction, PageActionDto>().ReverseMap();
            CreateMap<AddPageActionCommand, PageAction>().ReverseMap();
        }
    }
}
