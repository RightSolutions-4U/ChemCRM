using System;

namespace ChemWebsite.Data.Dto
{
    public class ChemicalIndustryDto
    {
        public Guid? ChemicalId { get; set; }
        public Guid IndustryId { get; set; }
        public ObjectState ObjectState { get; set; }
    }
}
