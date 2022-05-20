using ChemWebsite.Data.Dto;
using ChemWebsite.Helper;
using MediatR;
using System;

namespace ChemWebsite.MediatR.CommandAndQuery
{
    public class GetChemicalTypeQuery : IRequest<ServiceResponse<ChemicalTypeDto>>
    {
        public Guid Id { get; set; }
    }
}
