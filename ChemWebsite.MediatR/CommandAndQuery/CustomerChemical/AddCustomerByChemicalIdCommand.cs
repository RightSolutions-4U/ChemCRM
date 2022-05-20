using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChemWebsite.MediatR.CommandAndQuery
{
    public class AddCustomerByChemicalIdCommand : IRequest<bool>
    {
        public Guid ChemicalId { get; set; }
        public Guid CustomerId { get; set; }
    }
}
