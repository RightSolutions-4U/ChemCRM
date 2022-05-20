using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChemWebsite.Data.Entities
{
    public class Document : BaseEntity
    {
        public Guid Id { get; set; }
        public Guid CategoryId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }
        [ForeignKey("CategoryId")]
        public DocumentCategory Category { get; set; }
        public ICollection<DocumentUserPermission> DocumentUserPermissions { get; set; }
        public ICollection<DocumentRolePermission> DocumentRolePermissions { get; set; }
        public ICollection<DocumentAuditTrail> DocumentAuditTrails { get; set; }
    }
}
