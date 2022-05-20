using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChemWebsite.Data
{
    public class Industry : BaseEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }
        public List<ChemicalIndustry> ChemicalIndustries { get; set; } = new List<ChemicalIndustry>();
        [ForeignKey("CreatedBy")]
        public User CreatedByUser { get; set; }
        [ForeignKey("ModifiedBy")]
        public User ModifiedByUser { get; set; }
        [ForeignKey("DeletedBy")]
        public User DeletedByUser { get; set; }

    }
}
