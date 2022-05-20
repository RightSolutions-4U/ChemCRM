using AutoMapper;
using ChemWebsite.Data.Dto;
using ChemWebsite.Data.Entities;
using ChemWebsite.MediatR.CommandAndQuery;

namespace ChemWebsite.API.Helpers.Mapping
{
    public class InquiryNoteProfile : Profile
    {
        public InquiryNoteProfile()
        {
            CreateMap<AddInquiryNoteCommand, InquiryNote>();
            CreateMap<InquiryNoteDto, InquiryNote>().ReverseMap();
            CreateMap<UpdateInquiryNoteCommand, InquiryNote>();
        }
    }
}
