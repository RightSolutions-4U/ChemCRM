using ChemWebsite.Data;
using ChemWebsite.Data.Dto;
using MediatR;
using System;
using System.Collections.Generic;

namespace ChemWebsite.MediatR.CommandAndQuery
{
    public class GetPurchaseOrderDeliveryScheduleQuery
        : IRequest<List<PurchaseOrderDeliveryScheduleDto>>
    {
        public Guid PurchaseOrderId { get; set; }
    }
}
