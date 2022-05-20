using ChemWebsite.Data.Dto;
using MediatR;
using System;
using System.Collections.Generic;

namespace ChemWebsite.MediatR.CommandAndQuery
{
    public class GetChemicalsByCustomerQuery : IRequest<ChemicalListDto>
    {
        public Guid Id { get; set; }
        public int Skip { get; set; }
        public int Take { get; set; }
        public string ChemicalName { get; set; }
        public string CasNumber { get; set; }
    }
}
