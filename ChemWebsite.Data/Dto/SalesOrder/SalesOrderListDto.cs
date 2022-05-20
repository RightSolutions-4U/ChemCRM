using System;

namespace ChemWebsite.Data.Dto
{
    public class SalesOrderListDto
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string SalesOrderNumber { get; set; }
        public DateTime SalesOrderDate { get; set; }
        public Guid ChemicalId { get; set; }
        public string ChemicalName { get; set; }
        public int Quantity { get; set; }
        public decimal? Tax { get; set; }
        public decimal? Discount { get; set; }
        public decimal Rate { get; set; }
        public decimal Total { get; set; }
        public bool IsClosed { get; set; }
    }
}
