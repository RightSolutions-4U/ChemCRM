using ChemWebsite.Data.Dto;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChemWebsite.MediatR.CommandAndQuery
{
    public class GetAvailablePOForChemicalQuery : IRequest<List<PurchaseOrderDto>>
    {
        public Guid ChemicalId { get; set; }
    }
}
