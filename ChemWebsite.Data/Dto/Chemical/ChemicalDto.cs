using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ChemWebsite.Data.Dto
{
    public class ChemicalDto
    {
        public Guid? Id { get; set; }
        public Guid? ChemicalTypeId { get; set; }
        [Required(ErrorMessage = "Chemical Name Is Required")]
        public string Name { get; set; }
        public string CasNumber { get; set; }
        public string HBondAcceptor { get; set; }
        public string HBondDonor { get; set; }
        public string IUPACName { get; set; }
        public string InChIKey { get; set; }
        public string MolecularFormulla { get; set; }
        public string MolecularWeight { get; set; }
        public string Synonyms { get; set; }
        public string Url { get; set; }
        public string imageUrl { get; set; }
        public Guid? ChemicalDetailId { get; set; }
        public List<string> Categories { get; set; }
        public List<string> Industries { get; set; }
        public int SupplierCount { get; set; } = 0;
        public int CustomerCount { get; set; } = 0;
        public List<ChemicalIndustryDto> ChemicalIndustries { get; set; } = new List<ChemicalIndustryDto>();

    }
}
