using System;

namespace ChemWebsite.Data.Dto
{
    public class DocumentPermissionDto 
    {
        public Guid? Id { get; set; }
        public Guid DocumentId { get; set; }
        public Guid? RoleId { get; set; }
        public Guid? UserId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public RoleDto Role { get; set; }
        public UserDto User { get; set; }
        public string Type { get; set; }
        public bool IsTimeBound { get; set; }
        public bool IsAllowDownload { get; set; }
    }
}
