using MediatR;
using System.Collections.Generic;
using ChemWebsite.Data.Dto;

namespace ChemWebsite.MediatR.CommandAndQuery
{
    public class GetEmailSMTPSettingsQuery : IRequest<List<EmailSMTPSettingDto>>
    {
    }
}
