using ChemWebsite.Data.Dto;
using ChemWebsite.Helper;
using MediatR;
using System;

namespace ChemWebsite.MediatR.CommandAndQuery
{
    public class GetSupplierQuery : IRequest<ServiceResponse<SupplierDto>>
    {
        public Guid Id { get; set; }
    }
}
