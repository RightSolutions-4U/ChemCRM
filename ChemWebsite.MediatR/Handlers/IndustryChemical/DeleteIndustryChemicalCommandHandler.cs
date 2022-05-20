using ChemWebsite.Common.UnitOfWork;
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
    public class DeleteIndustryChemicalCommandHandler : IRequestHandler<DeleteIndustryChemicalCommand, ServiceResponse<IndustryDto>>
    {
        private readonly IIndustryChemicalRepository _industryChemicalRepository;
        private readonly IUnitOfWork<ChemWebsiteDbContext> _uow;
        private readonly ILogger<DeleteIndustryChemicalCommandHandler> _logger;

        public DeleteIndustryChemicalCommandHandler(
           IIndustryChemicalRepository industryChemicalRepository,
           IUnitOfWork<ChemWebsiteDbContext> uow,
           ILogger<DeleteIndustryChemicalCommandHandler> logger)
        {
            _industryChemicalRepository = industryChemicalRepository;
            _uow = uow;
            _logger = logger;
        }
        public async Task<ServiceResponse<IndustryDto>> Handle(DeleteIndustryChemicalCommand request, CancellationToken cancellationToken)
        {
            var industryChemical = await _industryChemicalRepository.All
                .Where(c => c.IndustryId == request.IndustryId && c.ChemicalId == request.ChemicalId)
                .FirstOrDefaultAsync();

            if (industryChemical == null)
            {
                _logger.LogError("Industry Product does not exists");
                return ServiceResponse<IndustryDto>.Return404();
            }

            _industryChemicalRepository.Remove(industryChemical);
            if (await _uow.SaveAsync() <= 0)
            {
                _logger.LogError("Error While saving Industry Product.");
                return ServiceResponse<IndustryDto>.Return500();
            }
            return ServiceResponse<IndustryDto>.ReturnSuccess();
        }
    }
}
