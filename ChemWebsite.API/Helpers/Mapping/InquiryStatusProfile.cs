using AutoMapper;
using ChemWebsite.Data;
using ChemWebsite.Data.Dto;
using ChemWebsite.Data.Entities;
using ChemWebsite.MediatR.CommandAndQuery;

namespace ChemWebsite.API.Helpers.Mapping
{
    public class InquiryStatusProfile : Profile
    {
        public InquiryStatusProfile()
        {
            CreateMap<InquiryStatus, InquiryStatusDto>().ReverseMap();
            CreateMap<AddInquiryStatusCommand, InquiryStatus>();
        }
    }
}
