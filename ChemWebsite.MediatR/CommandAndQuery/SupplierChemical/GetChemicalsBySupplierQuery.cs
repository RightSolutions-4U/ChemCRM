using ChemWebsite.Data.Dto;
using MediatR;
using System;

namespace ChemWebsite.MediatR.CommandAndQuery
{
    public class GetChemicalsBySupplierQuery : IRequest<ChemicalListDto>
    {
        public Guid Id { get; set; }
        public int Skip { get; set; }
        public int Take { get; set; }
        public string ChemicalName { get; set; }
        public string CasNumber { get; set; }
    }
}
