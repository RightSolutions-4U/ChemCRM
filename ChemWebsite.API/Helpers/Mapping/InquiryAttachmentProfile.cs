using AutoMapper;
using ChemWebsite.Data.Dto;
using ChemWebsite.Data.Entities;

namespace ChemWebsite.API.Helpers.Mapping
{
    public class InquiryAttachmentProfile : Profile
    {
        public InquiryAttachmentProfile()
        {
            CreateMap<InquiryAttachmentDto, InquiryAttachment>().ReverseMap();
        }
    }
}
