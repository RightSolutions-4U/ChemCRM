using MediatR;
using ChemWebsite.Data.Dto;
using ChemWebsite.Helper;

namespace ChemWebsite.MediatR.CommandAndQuery
{
    public class AddEmailTemplateCommand : IRequest<ServiceResponse<EmailTemplateDto>>
    {
        public string Name { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
    }
}
