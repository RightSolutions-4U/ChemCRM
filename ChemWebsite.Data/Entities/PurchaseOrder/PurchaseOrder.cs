using System;
using System.Collections.Generic;

namespace ChemWebsite.Data
{
    public class PurchaseOrder : BaseEntity
    {
        public Guid Id { get; set; }
        public string OrderNumber { get; set; }
        public DateTime POCreatedDate { get; set; }
        public DateTime? ClosedDate { get; set; }
        public bool IsClosed { get; set; }
        public Guid SupplierId { get; set; }
        public Guid ChemicalId { get; set; }
        public int TotalQuantity { get; set; }
        public int AvailableQuantity { get; set; } // Received Items
        public decimal TotalAmount { get; set; }
        public int InStockQuantity { get; set; } // After the Sales order Items
        public decimal PricePerUnit { get; set; }
        public decimal Tax { get; set; }
        public bool IsStockAtSupplierWarehouse { get; set; }
        public string SupplierInvoiceNumber { get; set; }
        public Guid PackagingTypeId { get; set; }
        public Supplier Supplier { get; set; }
        public Chemical Chemical { get; set; }
        public PackagingType PackagingType { get; set; }
        public ICollection<PurchaseOrderDeliverySchedule> PurchaseOrderDeliverySchedules { get; set; }
        public ICollection<SalesPurchaseOrderItem> SalesPurchaseOrderItems { get; set; }
        public ICollection<PurchaseOrderAttachment> PurchaseOrderAttachments { get; set; }
    }
}
