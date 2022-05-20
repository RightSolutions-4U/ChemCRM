using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChemWebsite.Data
{
    public class Chemical : BaseEntity
    {
        public Guid Id { get; set; }
        public Guid? ChemicalTypeId { get; set; }
        public string Name { get; set; }
        public string CasNumber { get; set; }
        public string HBondAcceptor { get; set; }
        public string HBondDonor { get; set; }
        public string IUPACName { get; set; }
        public string InChIKey { get; set; }
        public string MolecularFormulla { get; set; }
        public string MolecularWeight { get; set; }
        public string Synonyms { get; set; }
        public Guid? ChemicalDetailId { get; set; }
        [ForeignKey("ChemicalTypeId")]
        public ChemicalType ChemicalType { get; set; }
        public string Url { get; set; }
        [ForeignKey("CreatedBy")]
        public User CreatedByUser { get; set; }
        [ForeignKey("ModifiedBy")]
        public User ModifiedByUser { get; set; }
        [ForeignKey("DeletedBy")]
        public User DeletedByUser { get; set; }
        public ChemicalDetail ChemicalDetail { get; set; }
        public List<ChemicalSupplier> ChemicalSuppliers { get; set; } = new List<ChemicalSupplier>();
        public List<ChemicalCategory> ChemicalCategories { get; set; } = new List<ChemicalCategory>();
        public List<ChemicalIndustry> ChemicalIndustries { get; set; } = new List<ChemicalIndustry>();
        public List<ChemicalCustomer> ChemicalCustomers { get; set; } = new List<ChemicalCustomer>();
    }
}
