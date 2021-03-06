using ChemWebsite.Data.Dto;
using ChemWebsite.Helper;
using MediatR;
using System;

namespace ChemWebsite.MediatR.CommandAndQuery
{
    public class GetCustomerQuery : IRequest<ServiceResponse<CustomerDto>>
    {
        public Guid Id { get; set; }
    }
}
