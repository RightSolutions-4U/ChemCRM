using ChemWebsite.Data.Dto;
using MediatR;
using System;
using System.Collections.Generic;

namespace ChemWebsite.MediatR.CommandAndQuery
{
    public class GetRelatedChemicalsQuery : IRequest<List<RelatedChemicalDto>>
    {
        public Guid Id { get; set; }
    }
}
