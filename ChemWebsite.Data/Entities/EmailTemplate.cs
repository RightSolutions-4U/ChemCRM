using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChemWebsite.Data
{
    public class EmailTemplate: BaseEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        [ForeignKey("CreatedBy")]
        public User CreatedByUser { get; set; }
        [ForeignKey("ModifiedBy")]
        public User ModifiedByUser { get; set; }
        [ForeignKey("DeletedBy")]
        public User DeletedByUser { get; set; }
    }
}
