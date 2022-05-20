using ChemWebsite.Data.Dto;
using ChemWebsite.Helper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChemWebsite.MediatR.CommandAndQuery
{
    public class GetSalesOrderDetailQuery : IRequest<ServiceResponse<SalesOrderDto>>
    {
        public Guid Id { get; set; }
    }
}
