using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChemWebsite.Data.Entities
{
    public class InquiryAttachment : BaseEntity
    {
        public Guid Id { get; set; }
        public Guid InquiryId { get; set; }
        [ForeignKey("InquiryId")]
        public Inquiry Inquiry { get; set; }
        public string Path { get; set; }
        public string Name { get; set; }
    }
}
