using System;

namespace ChemWebsite.Data
{
    public class ChemicalSynonym
    {
        public Guid Id { get; set; }
        public string CasNumber { get; set; }
        public string Name { get; set; }
        public Guid ChemicalId { get; set; }
        public Chemical Chemical { get; set; }
    }
}
