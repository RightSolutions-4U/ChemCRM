﻿using ChemWebsite.Data.Dto;
using ChemWebsite.MediatR.CommandAndQuery;
using ChemWebsite.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ChemWebsite.MediatR.Handlers
{
    public  class SearchCustomerQueryHandler : IRequestHandler<SearchCustomerQuery, List<CustomerDto>>
    {
        private readonly ICustomerRepository _customerRepository;
        public SearchCustomerQueryHandler(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }
        public async Task<List<CustomerDto>> Handle(SearchCustomerQuery request, CancellationToken cancellationToken)
        {
            var customers = _customerRepository.All;
            if (!string.IsNullOrWhiteSpace(request.SearchQuery))
            {
                request.SearchQuery = request.SearchQuery.Trim();
            }
            else if (request.SearchQuery.ToLower() == "customerName")
            {
                customers = customers.Where(c => EF.Functions.Like(c.CustomerName, $"{request.SearchQuery}%"));
            }

            return await customers
                .OrderBy(c => c.CustomerName)
                .Select(c => new CustomerDto
                {
                    Id = c.Id,
                    CustomerName = c.CustomerName
                }).Take(request.PageSize)
            .AsNoTracking()
            .ToListAsync(cancellationToken);
        }
    }
}
