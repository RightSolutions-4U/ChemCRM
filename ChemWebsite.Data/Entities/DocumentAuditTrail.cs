using ChemWebsite.Data.Dto.Document;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChemWebsite.Data.Entities
{
    public class DocumentAuditTrail : BaseEntity
    {
        public Guid Id { get; set; }
        public Guid DocumentId { get; set; }
        [ForeignKey("DocumentId")]
        public Document Document { get; set; }
        public DocumentOperation OperationName { get; set; }
        public Guid? AssignToUserId { get; set; }
        public Guid? AssignToRoleId { get; set; }
        [ForeignKey("AssignToUserId")]
        public User AssignToUser { get; set; }
        [ForeignKey("AssignToRoleId")]
        public Role AssignToRole { get; set; }

    }
}
