using System;

namespace ChemWebsite.Data.Dto
{
    public class InquiryChemicalDto
    {
        public Guid ChemicalId { get; set; }
        public Guid InquiryId { get; set; }
        public string Name { get; set; }
        public string CasNumber { get; set; }
        public ChemicalDto Chemical { get; set; }
        public InquiryDto Inquiry { get; set; }
    }
}