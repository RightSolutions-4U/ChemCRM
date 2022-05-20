using AutoMapper;
using ChemWebsite.Common.UnitOfWork;
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
    public class UpdateIndustryCommandHandler : IRequestHandler<UpdateIndustryCommand, ServiceResponse<IndustryDto>>
    {

        private readonly IIndustryRepository _industryRepository;
        private readonly IUnitOfWork<ChemWebsiteDbContext> _uow;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateIndustryCommandHandler> _logger;

        public UpdateIndustryCommandHandler(
            IIndustryRepository industryRepository,
            IUnitOfWork<ChemWebsiteDbContext> uow,
            IMapper mapper,
            ILogger<UpdateIndustryCommandHandler> logger)
        {
            _industryRepository = industryRepository;
            _uow = uow;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<ServiceResponse<IndustryDto>> Handle(UpdateIndustryCommand request, CancellationToken cancellationToken)
        {
            var industryEntity = _industryRepository.Find(request.Id);
            _mapper.Map(request, industryEntity);
            _industryRepository.Update(industryEntity);
            if (await _uow.SaveAsync() <= 0)
            {
                _logger.LogError("Error while Updating Industry");
                return ServiceResponse<IndustryDto>.Return500();
            }
            return ServiceResponse<IndustryDto>.ReturnResultWith200(_mapper.Map<IndustryDto>(industryEntity));
        }
    }
}
