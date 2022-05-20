using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChemWebsite.MediatR.CommandAndQuery
{
    public class AddSupplierByChemicalIdCommand: IRequest<bool>
    {
        public Guid ChemicalId { get; set; }
        public Guid SupplierId { get; set; }
    }
}
