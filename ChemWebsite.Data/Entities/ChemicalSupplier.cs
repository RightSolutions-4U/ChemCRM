using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChemWebsite.Data
{
    public class ChemicalSupplier : BaseEntity
    {
        public Guid ChemicalId { get; set; }
        public Guid SupplierId { get; set; }
        public Chemical Chemical { get; set; }
        public Supplier Supplier { get; set; }
        [ForeignKey("CreatedBy")]
        public User CreatedByUser { get; set; }
        [ForeignKey("ModifiedBy")]
        public User ModifiedByUser { get; set; }
        [ForeignKey("DeletedBy")]
        public User DeletedByUser { get; set; }
    }
}
