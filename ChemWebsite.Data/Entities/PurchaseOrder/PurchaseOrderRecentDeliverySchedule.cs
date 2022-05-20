using System;

namespace ChemWebsite.Data.Entities
{
    public class PurchaseOrderRecentDeliverySchedule
    {
        public Guid PurchaseOrderId { get; set; }
        public string PurchaseOrderNumber { get; set; }
        public int ExpectedDispatchQuantity { get; set; }
        public DateTime ExpectedDispatchDate { get; set; }
        public Guid SupplierId { get; set; }
        public string SupplierName { get; set; }
        public Guid ChemicalId { get; set; }
        public string ChemicalName { get; set; }
    }
}
