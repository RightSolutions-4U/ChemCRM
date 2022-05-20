
using System;

namespace ChemWebsite.Data.Dto
{
    public class SalesOrderRecentShipmentDate
    {
        public Guid SalesOrderId { get; set; }
        public string SalesOrderNumber { get; set; }
        public DateTime ExpectedShipmentDate { get; set; }
        public int Quantity { get; set; }
        public Guid ChemicalId { get; set; }
        public string ChemicalName { get; set; }
        public Guid  CustomerId { get; set; }
        public string CustomerName { get; set; }
    }
}
