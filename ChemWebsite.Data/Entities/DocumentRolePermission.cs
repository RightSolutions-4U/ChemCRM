using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChemWebsite.Data.Entities
{
    public class DocumentRolePermission : BaseEntity
    {
        public Guid Id { get; set; }
        public Guid DocumentId { get; set; }
        public Guid RoleId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool IsTimeBound { get; set; }
        public bool IsAllowDownload { get; set; }
        [ForeignKey("DocumentId")]
        public Document Document { get; set; }
        [ForeignKey("RoleId")]
        public Role Role { get; set; }
    }
}
