using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChemWebsite.Data
{
    public class PurchaseOrderAttachment : BaseEntity
    {
        public Guid Id { get; set; }
        public Guid PurchaseOrderId { get; set; }
        [ForeignKey("PurchaseOrderId")]
        public PurchaseOrder PurchaseOrder { get; set; }
        public string Path { get; set; }
        public string Name { get; set; }
    }
}
