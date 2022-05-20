using AutoMapper;
using ChemWebsite.Data;
using ChemWebsite.Data.Dto;
using ChemWebsite.MediatR.CommandAndQuery;

namespace ChemWebsite.API.Helpers
{
    public class InquiryProfile: Profile
    {
        public InquiryProfile()
        {
            CreateMap<InquiryChemical, InquiryChemicalDto>().ReverseMap();
            CreateMap<Inquiry, InquiryDto>().ReverseMap();
            CreateMap<AddInquiryCommand, Inquiry>().ReverseMap();
            CreateMap<UpdateInquiryCommand, Inquiry>().ReverseMap();
        }
    }
}
