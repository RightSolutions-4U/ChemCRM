using ChemWebsite.Data.Dto;
using ChemWebsite.Data.Resources;
using MediatR;
using System;

namespace ChemWebsite.MediatR.CommandAndQuery
{
    public class GetSupplierByChemicalQuery : IRequest<SupplierListDto>
    {
        public SupplierResource SupplierResource { get; set; }
    }
}
