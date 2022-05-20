using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChemWebsite.Data
{
    public class SalesPurchaseOrderItem
    {
        public Guid SalesOrderId { get; set; }
        public Guid PurchaseOrderId { get; set; }
        public int Quantity { get; set; }
        [ForeignKey("SalesOrderId")]
        public SalesOrder SalesOrder { get; set; }
        [ForeignKey("PurchaseOrderId")]
        public PurchaseOrder PurchaseOrder { get; set; }
    }
}
