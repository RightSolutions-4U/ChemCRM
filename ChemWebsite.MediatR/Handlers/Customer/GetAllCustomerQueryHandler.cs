﻿using ChemWebsite.MediatR.CommandAndQuery;
using ChemWebsite.Repository;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ChemWebsite.MediatR.Handlers
{
    public class GetAllCustomerQueryHandler : IRequestHandler<GetAllCustomerQuery, CustomerList>
    {
        private readonly ICustomerRepository _customerRepository;

        public GetAllCustomerQueryHandler(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }
        public async Task<CustomerList> Handle(GetAllCustomerQuery request, CancellationToken cancellationToken)
        {
            return await _customerRepository.GetCustomers(request.CustomerResource);
        }
    }
}
