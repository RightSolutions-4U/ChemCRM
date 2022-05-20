using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChemWebsite.Data
{
    public class ChemicalCategory
    {
        public Guid Id { get; set; }
        public Guid ChemicalId { get; set; }
        public Guid CategoryId { get; set; }
        public Guid SubCategoryId { get; set; }
        public Category Category { get; set; }
        public Category SubCategory { get; set; }
        [ForeignKey("ChemicalId")]
        public Chemical Chemical { get; set; }
    }
}
