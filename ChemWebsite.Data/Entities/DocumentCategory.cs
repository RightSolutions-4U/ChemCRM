using System;

namespace ChemWebsite.Data.Entities
{
    public class DocumentCategory : BaseEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}