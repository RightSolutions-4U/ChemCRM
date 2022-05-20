using ChemWebsite.Data.Dto;
using ChemWebsite.Helper;
using MediatR;
using System;
using System.Collections.Generic;

namespace ChemWebsite.MediatR.CommandAndQuery
{
    public class AddSupplierChemicalCommand : IRequest<ServiceResponse<SupplierDto>>
    {
        public List<Guid> ChemicalIdList { get; set; }
        public Guid SupplierId { get; set; }
    }
}
