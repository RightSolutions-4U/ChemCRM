using ChemWebsite.Data.Dto;
using ChemWebsite.Helper;
using MediatR;
using System;

namespace ChemWebsite.MediatR.CommandAndQuery
{
    public class GetPurchaseOrderQuery : IRequest<ServiceResponse<PurchaseOrderDto>>
    {
        public Guid Id { get; set; }
    }
}
