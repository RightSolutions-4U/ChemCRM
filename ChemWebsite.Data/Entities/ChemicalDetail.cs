using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChemWebsite.Data
{
    public class ChemicalDetail : BaseEntity
    {
        public Guid Id { get; set; }
        public string Appearance { get; set; }
        public string Applications { get; set; }
        public string BoilingPoint { get; set; }
        public string DangerousGoodsInfo { get; set; }
        public string MeltingPoint { get; set; }
        public string Solubility { get; set; }
        public string Stability { get; set; }
        public string Storage { get; set; }
        [ForeignKey("CreatedBy")]
        public User CreatedByUser { get; set; }
        [ForeignKey("ModifiedBy")]
        public User ModifiedByUser { get; set; }
        [ForeignKey("DeletedBy")]
        public User DeletedByUser { get; set; }
    }
}
