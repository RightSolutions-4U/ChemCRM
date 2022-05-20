using ChemWebsite.Data.Dto;
using MediatR;
using System;

namespace ChemWebsite.MediatR.CommandAndQuery
{
    public class GetIndustryQuery: IRequest<IndustryDto>
    {
        public Guid Id { get; set; }
    }
}
