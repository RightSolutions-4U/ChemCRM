using System;

namespace ChemWebsite.Data.Dto
{
    public class ChemicalSupplierCountDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string CasNumber { get; set; }
        public ObjectState ObjectState { get; set; }
        public int? SupplierCount { get; set; }
    }
}
