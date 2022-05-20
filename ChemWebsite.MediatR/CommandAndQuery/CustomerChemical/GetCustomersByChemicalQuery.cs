using ChemWebsite.Data.Dto;
using MediatR;
using System;
using System.Collections.Generic;

namespace ChemWebsite.MediatR.CommandAndQuery
{
    public class GetCustomersByChemicalQuery : IRequest<List<CustomerDto>>
    {
        public Guid Id { get; set; }
    }
}
