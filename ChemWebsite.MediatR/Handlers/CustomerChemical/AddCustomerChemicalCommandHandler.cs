using AutoMapper;
using ChemWebsite.Common.UnitOfWork;
using ChemWebsite.Data;
using ChemWebsite.Data.Dto;
using ChemWebsite.Domain;
using ChemWebsite.Helper;
using ChemWebsite.MediatR.CommandAndQuery;
using ChemWebsite.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ChemWebsite.MediatR.Handlers
{
    public class AddCustomerChemicalCommandHandler : IRequestHandler<AddCustomerChemicalCommand, ServiceResponse<List<ChemicalDto>>>
    {
        private readonly IChemicalCustomerRepository _chemicalCustomerRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork<ChemWebsiteDbContext> _uow;
        private readonly ILogger<AddCustomerChemicalCommandHandler> _logger;

        public AddCustomerChemicalCommandHandler(IChemicalCustomerRepository chemicalCustomerRepository,
            IMapper mapper,
            IUnitOfWork<ChemWebsiteDbContext> uow,
            ILogger<AddCustomerChemicalCommandHandler> logger)
        {
            _chemicalCustomerRepository = chemicalCustomerRepository;
            _mapper = mapper;
            _uow = uow;
            _logger = logger;
        }
        public async Task<ServiceResponse<List<ChemicalDto>>> Handle(AddCustomerChemicalCommand request, CancellationToken cancellationToken)
        {
            foreach (Guid chemicalId in request.ChemicalIdList)
            {
                var chemicalCustomer = await _chemicalCustomerRepository.FindBy(c => c.CustomerId == request.CustomerId && c.ChemicalId == chemicalId).FirstOrDefaultAsync();
                if (chemicalCustomer == null)
                {
                    _chemicalCustomerRepository.Add(new ChemicalCustomer
                    {
                        CustomerId = request.CustomerId,
                        ChemicalId = chemicalId
                    });
                }
            }

            if (await _uow.SaveAsync() <= 0)
            {
                _logger.LogError("Error while updating Customer.");
                return ServiceResponse<List<ChemicalDto>>.Return500();
            }

            var chemicals = await _chemicalCustomerRepository.AllIncluding(c => c.Chemical)
                .Where(c => c.CustomerId == request.CustomerId)
                .Select(c => c.Chemical)
                .ToListAsync();
            return ServiceResponse<List<ChemicalDto>>.ReturnResultWith200(_mapper.Map<List<ChemicalDto>>(chemicals));
        }
    }
}
