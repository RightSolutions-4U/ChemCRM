using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChemWebsite.Data
{
    public class ChemicalIndustry
    {
        public Guid ChemicalId { get; set; }
        public Guid IndustryId { get; set; }
        public Chemical Chemical { get; set; }
        public Industry Industry { get; set; }
        [NotMapped]
        public ObjectState ObjectState { get; set; }
    }
}
