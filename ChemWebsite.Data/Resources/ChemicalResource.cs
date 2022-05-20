using System;
using ChemWebsite.Helper;

namespace ChemWebsite.Data
{
    public class ChemicalResource : ResourceParameters
    {
        public ChemicalResource():base("Name")
        {
        }
        public Guid? ChemicalId { get; set; }
        public string CasNumber { get; set; }
        public string Name { get; set; }
    }
}
