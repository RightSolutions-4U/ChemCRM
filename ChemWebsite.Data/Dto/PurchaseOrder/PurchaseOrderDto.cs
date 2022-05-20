using System;
using System.Collections.Generic;

namespace ChemWebsite.Data.Dto
{
    public class PurchaseOrderDto
    {
        public Guid Id { get; set; }
        public string OrderNumber { get; set; }
        public DateTime POCreatedDate { get; set; }
        public DateTime? ClosedDate { get; set; }
        public bool IsClosed { get; set; }
        public Guid SupplierId { get; set; }
        public Guid ChemicalId { get; set; }
        public int TotalQuantity { get; set; }
        public int AvailableQuantity { get; set; }
        public int InStockQuantity { get; set; }
        public decimal PricePerUnit { get; set; }
        public decimal Tax { get; set; }
        public bool IsStockAtSupplierWarehouse { get; set; }
        public string SupplierInvoiceNumber { get; set; }
        public string SupplierName { get; set; }
        public string ChemicalName { get; set; }
        public string PackagingTypeName { get; set; }
        public decimal TotalAmount { get; set; }
        public Guid PackagingTypeId { get; set; }
        public SupplierDto Supplier { get; set; }
        public ChemicalDto Chemical { get; set; }
        public PackagingTypeDto PackagingType { get; set; }
       
        public List<PurchaseOrderDeliveryScheduleDto> PurchaseOrderDeliverySchedules { get; set; }
        public List<SalesPurchaseOrderItemDto> SalesPurchaseOrderItems { get; set; }
        public List<PurchaseOrderAttachmentDto> PurchaseOrderAttachments { get; set; }
    }
}
