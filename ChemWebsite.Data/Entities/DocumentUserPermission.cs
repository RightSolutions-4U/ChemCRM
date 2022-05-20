using System;
using System.ComponentModel.DataAnnotations.Schema;


namespace ChemWebsite.Data.Entities
{
    public class DocumentUserPermission : BaseEntity
    {
        public Guid Id { get; set; }
        public Guid DocumentId { get; set; }
        public Guid UserId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool IsTimeBound { get; set; }
        public bool IsAllowDownload { get; set; }
        [ForeignKey("DocumentId")]
        public Document Document { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }
    }
}
