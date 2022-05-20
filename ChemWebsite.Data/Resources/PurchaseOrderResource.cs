using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChemWebsite.Data.Resources
{
    public class PurchaseOrderResource : ResourceParameter
    {
        public PurchaseOrderResource() : base("POCreatedDate")
        {
        }

        public string OrderNumber { get; set; }
        public string SupplierName { get; set; }
        public string ChemicalName { get; set; }
        public DateTime? POCreatedDate { get; set; }
        public Guid? SupplierId { get; set; }
    }
}
