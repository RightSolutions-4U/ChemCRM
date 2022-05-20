using ChemWebsite.Data.Dto;
using ChemWebsite.Helper;
using MediatR;
using System;

namespace ChemWebsite.MediatR.CommandAndQuery
{
    public class UpdateDeliveryScheduleCommand : IRequest<ServiceResponse<PurchaseOrderDeliveryScheduleDto>>
    {
        public Guid Id { get; set; }
        public Guid PurchaseOrderId { get; set; }
        public DateTime ExpectedDispatchDate { get; set; }
        public DateTime? ActualDispatchDate { get; set; }
        public int ExpectedDispatchQuantity { get; set; }
        public int? ActualDispatchQuantity { get; set; }
        public bool IsReceived { get; set; }
    }
}
