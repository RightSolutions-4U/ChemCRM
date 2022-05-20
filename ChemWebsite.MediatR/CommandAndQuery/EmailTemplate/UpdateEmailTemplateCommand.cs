using MediatR;
using System;
using ChemWebsite.Data.Dto;
using ChemWebsite.Helper;

namespace ChemWebsite.MediatR.CommandAndQuery
{
    public class UpdateEmailTemplateCommand : IRequest<ServiceResponse<EmailTemplateDto>>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
    }
}
