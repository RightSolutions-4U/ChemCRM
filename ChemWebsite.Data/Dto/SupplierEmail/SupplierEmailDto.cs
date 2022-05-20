using System;

namespace ChemWebsite.Data.Dto
{
    public class SupplierEmailDto
    {
        public Guid? Id { get; set; }
        public Guid? SupplierId { get; set; }
        public string Email { get; set; }
    }
}
