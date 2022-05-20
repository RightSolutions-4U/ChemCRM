using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChemWebsite.Data
{
    public class Category : BaseEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid? ParentId { get; set; }
        public string Description { get; set; }
        public int CatLevel { get; set; }
        [ForeignKey("ModifiedBy")]
        public User ModifiedByUser { get; set; }
        [ForeignKey("DeletedBy")]
        public User DeletedByUser { get; set; }
        public List<ChemicalCategory> ChemicalCategories { get; set; } = new List<ChemicalCategory>();
        public List<ChemicalCategory> ChemicalSubCategories { get; set; } = new List<ChemicalCategory>();
    }
}
