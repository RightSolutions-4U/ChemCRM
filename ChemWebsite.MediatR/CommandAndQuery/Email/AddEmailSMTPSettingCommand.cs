using MediatR;
using ChemWebsite.Data.Dto;
using ChemWebsite.Helper;

namespace ChemWebsite.MediatR.CommandAndQuery
{
    public class AddEmailSMTPSettingCommand : IRequest<ServiceResponse<EmailSMTPSettingDto>>
    {
        public string Host { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool IsEnableSSL { get; set; }
        public int Port { get; set; }
        public bool IsDefault { get; set; }
    }
}
