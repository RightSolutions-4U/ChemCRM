using MediatR;
using System;
using ChemWebsite.Data.Dto;
using ChemWebsite.Helper;

namespace ChemWebsite.MediatR.CommandAndQuery
{
    public class GetEmailTemplateQuery : IRequest<ServiceResponse<EmailTemplateDto>>
    {
        public Guid Id { get; set; }
    }
}
