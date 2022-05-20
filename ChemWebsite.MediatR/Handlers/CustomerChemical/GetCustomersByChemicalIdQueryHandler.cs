using AutoMapper;
using ChemWebsite.Data.Dto;
using ChemWebsite.MediatR.CommandAndQuery;
using ChemWebsite.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace ChemWebsite.MediatR.Handlers
{
    public class GetCustomersByChemicalIdQueryHandler : IRequestHandler<GetCustomersByChemicalIdQuery, CustomerListDto>
    {
        private readonly IChemicalCustomerRepository _chemicalCustomerRepository;
        private readonly IMapper _mapper;

        public GetCustomersByChemicalIdQueryHandler(IChemicalCustomerRepository chemicalCustomerRepository,
            IMapper mapper)
        {
            _chemicalCustomerRepository = chemicalCustomerRepository;
            _mapper = mapper;
        }

        public async Task<CustomerListDto> Handle(GetCustomersByChemicalIdQuery request, CancellationToken cancellationToken)
        {
            var customerQuery = _chemicalCustomerRepository.AllIncluding(c => c.Customer)
               .OrderBy(c => c.Customer.CustomerName)
               .Where(c => !c.Customer.IsDeleted && c.ChemicalId == request.CustomerResource.ChemicalId);

            if (!string.IsNullOrWhiteSpace(request.CustomerResource.CustomerName))
            {
                var genreForWhereClause = request.CustomerResource.CustomerName.Trim().ToLowerInvariant();
                var name = Uri.UnescapeDataString(genreForWhereClause);
                var encodingName = WebUtility.UrlDecode(name);
                var ecapestring = Regex.Unescape(encodingName);
                encodingName = encodingName.Replace(@"\", @"\\").Replace("%", @"\%").Replace("_", @"\_").Replace("[", @"\[").Replace(" ", "%");
                customerQuery = customerQuery.Where(a => EF.Functions.Like(a.Customer.CustomerName, $"{encodingName}%", @"\"));
            }

            if (!string.IsNullOrEmpty(request.CustomerResource.MobileNo))
            {
                // trim & ignore casing
                var searchQueryForWhereClause = request.CustomerResource.MobileNo
                    .Trim().ToLowerInvariant();
                customerQuery = customerQuery
                    .Where(a => (a.Customer.MobileNo != null && EF.Functions.Like(a.Customer.MobileNo, $"%{searchQueryForWhereClause}%")) ||
                    (a.Customer.PhoneNo != null && EF.Functions.Like(a.Customer.PhoneNo, $"%{searchQueryForWhereClause}%")));
            }

            if (!string.IsNullOrEmpty(request.CustomerResource.Email))
            {
                // trim & ignore casing
                var searchQueryForWhereClause = request.CustomerResource.Email
                    .Trim().ToLowerInvariant();
                customerQuery = customerQuery
                    .Where(a => EF.Functions.Like(a.Customer.Email, $"{searchQueryForWhereClause}%"));
            }

            var customers = await customerQuery
                .Select(c => c.Customer)
                .Skip(request.CustomerResource.Skip)
                .Take(request.CustomerResource.PageSize)
                .ToListAsync();
            var customerDtos = _mapper.Map<List<CustomerDto>>(customers);
            var result = new CustomerListDto
            {
                Customers = customerDtos,
                TotalCount = customerQuery.Count()
            };
            return result;
        }
    }
}
