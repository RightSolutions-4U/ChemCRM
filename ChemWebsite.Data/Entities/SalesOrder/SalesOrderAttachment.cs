using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChemWebsite.Data
{
    public class SalesOrderAttachment : BaseEntity
    {
        public Guid Id { get; set; }
        public Guid SalesOrderId { get; set; }
        [ForeignKey("SalesOrderId")]
        public SalesOrder SalesOrder { get; set; }
        public string Path { get; set; }
        public string Name { get; set; }
    }
}
