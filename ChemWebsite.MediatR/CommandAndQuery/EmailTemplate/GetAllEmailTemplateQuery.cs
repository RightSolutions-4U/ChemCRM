using MediatR;
using System.Collections.Generic;
using ChemWebsite.Data.Dto;
using ChemWebsite.Helper;

namespace ChemWebsite.MediatR.CommandAndQuery
{
    public class GetAllEmailTemplateQuery : IRequest<ServiceResponse<List<EmailTemplateDto>>>
    {

    }
}
