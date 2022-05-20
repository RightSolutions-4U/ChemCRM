using AutoMapper;
using ChemWebsite.Data.Dto;
using ChemWebsite.Data.Entities;
using ChemWebsite.MediatR.CommandAndQuery;

namespace ChemWebsite.API.Helpers.Mapping
{
    public class InquiryActivityProfile : Profile
    {
        public InquiryActivityProfile()
        {
            CreateMap<InquiryActivity, InquiryActivityDto>().ReverseMap();
            CreateMap<AddInquiryActivityCommand, InquiryActivity>();
            CreateMap<UpdateInquiryActivityCommand, InquiryActivity>();
        }
    }
}
