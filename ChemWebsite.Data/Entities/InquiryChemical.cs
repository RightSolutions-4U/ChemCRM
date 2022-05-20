using System;

namespace ChemWebsite.Data
{
    public class InquiryChemical
    {
        public Guid ChemicalId { get; set; }
        public Guid InquiryId { get; set; }
        public Chemical Chemical { get; set; }
        public Inquiry Inquiry { get; set; }
    }
}
