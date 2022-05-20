using AutoMapper;
using ChemWebsite.Data;
using ChemWebsite.Data.Dto;
using ChemWebsite.MediatR.CommandAndQuery;

namespace ChemWebsite.API.Helpers.Mapping
{
    public class EmailProfile : Profile
    {
        public EmailProfile()
        {
            CreateMap<EmailSMTPSetting, EmailSMTPSettingDto>().ReverseMap();
            CreateMap<EmailSMTPSetting, AddEmailSMTPSettingCommand>().ReverseMap();
            CreateMap<EmailSMTPSetting, UpdateEmailSMTPSettingCommand>().ReverseMap();
        }
    }
}
