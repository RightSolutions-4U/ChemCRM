using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChemWebsite.Data.Dto
{
    public class PurchaseOrderAttachmentDto
    {
        public Guid? Id { get; set; }
        public Guid? PurchaseOrderId { get; set; }
        public string Path { get; set; }
        public string Name { get; set; }
        public UserDto CreatedByUser { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string DocumentData { get; set; }
    }
}
