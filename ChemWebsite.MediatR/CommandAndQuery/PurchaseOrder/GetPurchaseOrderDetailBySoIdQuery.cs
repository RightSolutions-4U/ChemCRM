using ChemWebsite.Data.Dto;
using ChemWebsite.Helper;
using MediatR;
using System;
using System.Collections.Generic;

namespace ChemWebsite.MediatR.CommandAndQuery
{
    public class GetPurchaseOrderDetailBySoIdQuery : IRequest<ServiceResponse<List<PurchaseOrderShort>>>
    {
        public Guid SalesOrderId { get; set; }
    }
}
