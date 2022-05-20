using System;
using System.Collections.Generic;

namespace ChemWebsite.Data.Dto
{
    public class SalesOrderDto
    {
        public Guid? Id { get; set; }
        public Guid CustomerId { get; set; }
        public CustomerDto Customer { get; set; } = null;
        public string SalesOrderNumber { get; set; }
        public string Reference { get; set; }
        public DateTime SalesOrderDate { get; set; }
        public DateTime ExpectedShipmentDate { get; set; }
        public Guid? PaymentTermId { get; set; }
        public Guid? DeliveryMethodId { get; set; }
        public Guid ChemicalId { get; set; }
        public int Quantity { get; set; }
        public decimal Rate { get; set; }
        public decimal? Discount { get; set; }
        public decimal? Tax { get; set; }
        public decimal Total { get; set; }
        public bool IsClosed { get; set; }
        public DateTime? ClosedDate { get; set; }
        public bool IsInvoiceGenerated { get; set; }
        public DateTime? InvoiceGeneratedDate { get; set; }
        public string CustomerNote { get; set; }
        public string TermsAndConditions { get; set; }
        public string PaymentTermName { get; set; }
        public string DeliveryMethodName { get; set; }
        public string ChemicalName { get; set; }
        public string CustomerName { get; set; }
        public ICollection<SalesPurchaseOrderItemDto> SalesPurchaseOrderItems { get; set; }
        public ICollection<SalesOrderAttachmentDto> SalesOrderAttachments { get; set; }
    }
}
