using AutoMapper;
using ChemWebsite.Data;
using ChemWebsite.Data.Dto;
using ChemWebsite.MediatR.CommandAndQuery;

namespace ChemWebsite.API.Helpers
{
    public class InquirySourceProfile : Profile
    {
        public InquirySourceProfile()
        {
            CreateMap<InquirySource, InquirySourceDto>().ReverseMap();
            CreateMap<AddInquirySourceCommand, InquirySource>();
            CreateMap<UpdateInquirySourceCommand, InquirySource>();
        }
    }
}
