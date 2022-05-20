using System;

namespace ChemWebsite.Data.Dto
{
    public class InventoryDto
    {
        public Guid ChemicalId { get; set; }
        public string ChemicalName { get; set; }
        public string CasNo { get; set; }
        public int Quantity { get; set; }
    }
}
