using ChemWebsite.Data.Dto;
using ChemWebsite.Helper;
using MediatR;
using System;

namespace ChemWebsite.MediatR.CommandAndQuery
{
    public class DeleteSupplierChemicalCommand : IRequest<ServiceResponse<SupplierDto>>
    {
        public Guid SupplierId { get; set; }
        public Guid ChemicalId { get; set; }
    }
}
