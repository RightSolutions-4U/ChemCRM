using ChemWebsite.Data.Dto;
using MediatR;
using System.Collections.Generic;
using ChemWebsite.Helper;

namespace ChemWebsite.MediatR.CommandAndQuery
{
    public class GetAllActionQuery : IRequest<ServiceResponse<List<ActionDto>>>
    {
    }
}
