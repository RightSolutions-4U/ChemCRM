using AutoMapper;
using ChemWebsite.Data;
using ChemWebsite.Data.Dto;
using ChemWebsite.MediatR.CommandAndQuery;

namespace ChemWebsite.API.Helpers
{
    public class ContactUsMapping: Profile
    {
        public ContactUsMapping()
        {
            CreateMap<ContactRequest, ContactUsDto>().ReverseMap();
            CreateMap<AddContactUsCommand, ContactRequest>();
        }
    }
}
