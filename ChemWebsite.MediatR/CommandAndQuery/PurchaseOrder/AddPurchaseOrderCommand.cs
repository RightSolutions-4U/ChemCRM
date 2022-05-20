using ChemWebsite.Data.Dto;
using ChemWebsite.Helper;
using MediatR;
using System;
using System.Collections.Generic;

namespace ChemWebsite.MediatR.CommandAndQuery
{
    public class AddPurchaseOrderCommand : IRequest<ServiceResponse<PurchaseOrderDto>>
    {
        public string OrderNumber { get; set; }
        public DateTime POCreatedDate { get; set; }
        public Guid SupplierId { get; set; }
        public Guid ChemicalId { get; set; }
        public int TotalQuantity { get; set; }
        public decimal PricePerUnit { get; set; }
        public decimal Tax { get; set; }
        public decimal TotalAmount { get; set; }
        public bool IsStockAtSupplierWarehouse { get; set; }
        public string SupplierInvoiceNumber { get; set; }
        public Guid PackagingTypeId { get; set; }
        public ICollection<PurchaseOrderDeliveryScheduleDto> PurchaseOrderDeliverySchedules { get; set; }
        public List<PurchaseOrderAttachmentDto> PurchaseOrderAttachments { get; set; } = new List<PurchaseOrderAttachmentDto>();
    }
}
