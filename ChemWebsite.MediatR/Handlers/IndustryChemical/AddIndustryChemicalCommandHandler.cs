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
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ChemWebsite.MediatR.Handlers
{
    public class AddIndustryChemicalCommandHandler : IRequestHandler<AddIndustryChemicalCommand, ServiceResponse<IndustryDto>>
    {
        private readonly IIndustryChemicalRepository _industryChemicalRepository;
        private readonly IUnitOfWork<ChemWebsiteDbContext> _uow;
        private readonly ILogger<AddIndustryChemicalCommandHandler> _logger;

        public AddIndustryChemicalCommandHandler(
            IIndustryChemicalRepository industryChemicalRepository,
            IUnitOfWork<ChemWebsiteDbContext> uow,
            ILogger<AddIndustryChemicalCommandHandler> logger)
        {
            _industryChemicalRepository = industryChemicalRepository;
            _uow = uow;
            _logger = logger;
        }
        public async Task<ServiceResponse<IndustryDto>> Handle(AddIndustryChemicalCommand request, CancellationToken cancellationToken)
        {
            var isIndustryChemicalAdded = false;
            foreach (Guid chemicalId in request.ChemicalIdList.Distinct())
            {
                var industryChemicals = await _industryChemicalRepository.FindBy(c => c.IndustryId == request.IndustryId && c.ChemicalId == chemicalId).FirstOrDefaultAsync();
                if (industryChemicals == null)
                {
                    isIndustryChemicalAdded = true;
                    _industryChemicalRepository.Add(new ChemicalIndustry()
                    {
                        IndustryId = request.IndustryId,
                        ChemicalId = chemicalId
                    });
                }
            }

            if (isIndustryChemicalAdded && await _uow.SaveAsync() <= 0)
            {
                _logger.LogError("Error While saving industry Products.");
                return ServiceResponse<IndustryDto>.Return500();
            }
            return ServiceResponse<IndustryDto>.ReturnSuccess();
        }
    }
}
