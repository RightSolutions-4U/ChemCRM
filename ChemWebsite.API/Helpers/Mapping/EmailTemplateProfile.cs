using AutoMapper;
using ChemWebsite.Data;
using ChemWebsite.Data.Dto;
using ChemWebsite.MediatR.CommandAndQuery;

namespace ChemWebsite.API.Helpers
{
    public class EmailTemplateProfile : Profile
    {
        public EmailTemplateProfile()
        {
            CreateMap<EmailTemplateDto, EmailTemplate>().ReverseMap();
            CreateMap<AddEmailTemplateCommand, EmailTemplate>();
            CreateMap<UpdateEmailTemplateCommand, EmailTemplate>().ReverseMap();
        }
    }
}
