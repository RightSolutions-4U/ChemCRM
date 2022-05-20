using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChemWebsite.Data
{
    public class Page : BaseEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        [ForeignKey("CreatedBy")]
        public User CreatedByUser { get; set; }
        [ForeignKey("ModifiedBy")]
        public User ModifiedByUser { get; set; }
        [ForeignKey("DeletedBy")]
        public User DeletedByUser { get; set; }
        public int Order { get; set; }
    }
}
