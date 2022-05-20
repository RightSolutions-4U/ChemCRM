using System;

namespace ChemWebsite.Data.Resources
{
    public class SaleOrderResource: ResourceParameter
    {
        public SaleOrderResource() : base("SalesOrderDate")
        {
        }
        public Guid? CustomerId { get; set; }
        public string SalesOrderNumber { get; set; }
        public DateTime? SalesOrderDate { get; set; }
        public Guid? ChemicalId { get; set; }

    }
}
