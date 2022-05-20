using System.Collections.Generic;
using MediatR;
using ChemWebsite.Data.Dto;

namespace ChemWebsite.MediatR.CommandAndQuery
{
    public class GetIndustriesQuery : IRequest<List<IndustryDto>>
    {

    }
}
