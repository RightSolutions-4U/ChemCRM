using AutoMapper;
using ChemWebsite.Common.UnitOfWork;
using ChemWebsite.Data.Dto;
using ChemWebsite.Domain;
using ChemWebsite.Helper;
using ChemWebsite.MediatR.CommandAndQuery;
using ChemWebsite.Repository;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ChemWebsite.MediatR.Handlers
{
    public class DeleteIndustryCommandHandler : IRequestHandler<DeleteIndustryCommand, ServiceResponse<IndustryDto>>
    {
        private readonly IIndustryRepository _industryRepository;
        private readonly IUnitOfWork<ChemWebsiteDbContext> _uow;
        private readonly ILogger<DeleteIndustryCommandHandler> _logger;


        public DeleteIndustryCommandHandler(
            IIndustryRepository industryRepository,
            IUnitOfWork<ChemWebsiteDbContext> uow,
            ILogger<DeleteIndustryCommandHandler> logger)
        {
            _industryRepository = industryRepository;
            _uow = uow;
            _logger = logger;
        }

        public async Task<ServiceResponse<IndustryDto>> Handle(DeleteIndustryCommand request, CancellationToken cancellationToken)
        {
            var industry = await _industryRepository.FindAsync(request.Id);
            if (industry == null)
            {
                _logger.LogError("Industry does not exists");
                return ServiceResponse<IndustryDto>.Return404();
            }

            industry.IsDeleted = true;
            _industryRepository.Update(industry);

            if (await _uow.SaveAsync() <= 0)
            {
                _logger.LogError("Error while Deleting Industry");
                return ServiceResponse<IndustryDto>.Return500();
            }
            return ServiceResponse<IndustryDto>.ReturnSuccess();
        }
    }
}
