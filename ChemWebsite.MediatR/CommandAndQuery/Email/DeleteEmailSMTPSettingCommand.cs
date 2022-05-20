using MediatR;
using System;
using ChemWebsite.Data.Dto;
using ChemWebsite.Helper;

namespace ChemWebsite.MediatR.CommandAndQuery
{
    public class DeleteEmailSMTPSettingCommand : IRequest<ServiceResponse<EmailSMTPSettingDto>>
    {
        public Guid Id { get; set; }
    }
}
