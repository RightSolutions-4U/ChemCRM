using System;

namespace ChemWebsite.Data.Dto
{
    public class DocumentRolePermissionDto 
    {
        public Guid? Id { get; set; }
        public Guid DocumentId { get; set; }
        public Guid RoleId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool IsTimeBound { get; set; }
        public bool IsAllowDownload { get; set; }
        public RoleDto Role { get; set; }
    }
}
