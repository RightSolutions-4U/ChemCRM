using ChemWebsite.Data;
using ChemWebsite.Data.Dto;
using ChemWebsite.Repository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChemWebsite.MediatR.CommandAndQuery
{
    public class GetCustomersByChemicalIdQuery : IRequest<CustomerListDto>
    {
        public CustomerResource CustomerResource { get; set; }
    }
}
