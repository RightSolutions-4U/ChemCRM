using System;

namespace ChemWebsite.Data
{
    public class SupplierEmail 
    {
        public Guid Id { get; set; }
        public Guid SupplierId { get; set; }
        public string Email { get; set; }
      
    }
}
