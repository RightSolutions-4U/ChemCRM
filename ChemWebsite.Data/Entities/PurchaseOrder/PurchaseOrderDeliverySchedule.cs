using System;

namespace ChemWebsite.Data
{
    public class PurchaseOrderDeliverySchedule
    {
        public Guid Id { get; set; }
        public Guid PurchaseOrderId { get; set; }
        public DateTime ExpectedDispatchDate { get; set; }
        public DateTime? ActualDispatchDate { get; set; }
        public int ExpectedDispatchQuantity { get; set; }
        public int? ActualDispatchQuantity { get; set; }
        public bool IsReceived { get; set; }
        public PurchaseOrder PurchaseOrder { get; set; }
    }
}
