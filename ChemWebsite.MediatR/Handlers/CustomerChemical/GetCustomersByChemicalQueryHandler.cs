using AutoMapper;
using ChemWebsite.Data.Dto;
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
    public class GetCustomersByChemicalQueryHandler : IRequestHandler<GetCustomersByChemicalQuery, List<CustomerDto>>
    {
        private readonly IChemicalCustomerRepository _chemicalCustomerRepository;
        private readonly IMapper _mapper;

        public GetCustomersByChemicalQueryHandler(IChemicalCustomerRepository chemicalCustomerRepository,
            IMapper mapper)
        {
            _chemicalCustomerRepository = chemicalCustomerRepository;
            _mapper = mapper;
        }
        public async Task<List<CustomerDto>> Handle(GetCustomersByChemicalQuery request, CancellationToken cancellationToken)
        {
            var customers = await _chemicalCustomerRepository.AllIncluding(c => c.Customer)
                .Where(c => c.ChemicalId == request.Id)
                 .Select(c => c.Customer)
                 .ToListAsync();
            return _mapper.Map<List<CustomerDto>>(customers);
        }
    }
}
