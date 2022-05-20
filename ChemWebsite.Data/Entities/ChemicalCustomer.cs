using System;

namespace ChemWebsite.Data
{
    public class ChemicalCustomer
    {
        public Guid ChemicalId { get; set; }
        public Guid CustomerId { get; set; }
        public Chemical Chemical { get; set; }
        public Customer Customer { get; set; }
    }
}
