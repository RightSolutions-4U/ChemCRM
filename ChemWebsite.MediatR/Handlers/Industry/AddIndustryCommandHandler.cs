using AutoMapper;
using ChemWebsite.Common.UnitOfWork;
using ChemWebsite.Data;
using ChemWebsite.Data.Dto;
using ChemWebsite.Domain;
using ChemWebsite.Helper;
using ChemWebsite.MediatR.Command;
using ChemWebsite.Repository;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace ChemWebsite.MediatR.Handler
{
    public class AddIndustryCommandHandler : IRequestHandler<AddIndustryCommand, ServiceResponse<IndustryDto>>
    {
        private readonly IIndustryRepository _industryRepository;
        private readonly IUnitOfWork<ChemWebsiteDbContext> _uow;
        private readonly IMapper _mapper;
        private readonly ILogger<AddIndustryCommandHandler> _logger;

        public AddIndustryCommandHandler(
            IIndustryRepository industryRepository,
            IUnitOfWork<ChemWebsiteDbContext> uow,
            IMapper mapper,
            ILogger<AddIndustryCommandHandler> logger)
        {
            _industryRepository = industryRepository;
            _uow = uow;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<ServiceResponse<IndustryDto>> Handle(AddIndustryCommand request, CancellationToken cancellationToken)
        {
            var industryEntity = _mapper.Map<Industry>(request);
            _industryRepository.Add(industryEntity);
            if (await _uow.SaveAsync() <= 0)
            {
                _logger.LogError("Error while adding industry");
                return ServiceResponse<IndustryDto>.Return500();
            }
            var industrydto = _mapper.Map<IndustryDto>(industryEntity);
            return ServiceResponse<IndustryDto>.ReturnResultWith200(industrydto);
        }

    }
}
