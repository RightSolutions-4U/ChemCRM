using System;
using System.Collections.Generic;

namespace ChemWebsite.Data
{
    public class SalesOrder : BaseEntity
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
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
        public ICollection<SalesPurchaseOrderItem> SalesPurchaseOrderItems { get; set; }
        public PaymentTerm PaymentTerm { get; set; }
        public DeliveryMethod DeliveryMethod { get; set; }
        public Chemical Chemical { get; set; }
        public Customer Customer { get; set; }
        public ICollection<SalesOrderAttachment> SalesOrderAttachments { get; set; }

    }
}
