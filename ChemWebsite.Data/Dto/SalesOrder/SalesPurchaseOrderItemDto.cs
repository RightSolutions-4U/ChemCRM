using System;

namespace ChemWebsite.Data.Dto
{
    public class SalesPurchaseOrderItemDto
    {
        public Guid? SalesOrderId { get; set; }
        public Guid PurchaseOrderId { get; set; }
        public int Quantity { get; set; }
        public SalesOrderDto SalesOrder { get; set; } = null;
        public string PurchaseOrderNumber { get; set; }
        public string SupplierName { get; set; }
        public decimal PricePerUnit { get; set; }
    }
}
