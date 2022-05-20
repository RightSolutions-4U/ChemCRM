using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChemWebsite.Data.Entities
{
    public class InquiryNote: BaseEntity
    {
        public Guid Id { get; set; }
        public Guid InquiryId { get; set; }
        [ForeignKey("InquiryId")]
        public Inquiry Inquiry { get; set; }
        public string Note { get; set; }

    }
}
