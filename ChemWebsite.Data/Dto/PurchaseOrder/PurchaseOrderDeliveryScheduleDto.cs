using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChemWebsite.Data.Dto
{
   public class PurchaseOrderDeliveryScheduleDto
    {
        public Guid? Id { get; set; }
        public Guid? PurchaseOrderId { get; set; }
        public DateTime ExpectedDispatchDate { get; set; }
        public DateTime? ActualDispatchDate { get; set; }
        public int ExpectedDispatchQuantity { get; set; }
        public int? ActualDispatchQuantity { get; set; }
        public bool IsReceived { get; set; }
    }
}
