using ChemWebsite.Common.UnitOfWork;
using ChemWebsite.Domain;
using ChemWebsite.Helper;
using ChemWebsite.MediatR.CommandAndQuery;
using ChemWebsite.Repository;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace ChemWebsite.MediatR.Handlers
{
    public class RemoveChemicalImageCommandHandler : IRequestHandler<RemoveChemicalImageCommand, ServiceResponse<string>>
    {
        private readonly IUnitOfWork<ChemWebsiteDbContext> _uow;
        private readonly IChemicalRepository _chemicalRepository;
        private readonly ILogger<RemoveChemicalImageCommandHandler> _logger;

        public RemoveChemicalImageCommandHandler(IChemicalRepository chemicalRepository,
            IUnitOfWork<ChemWebsiteDbContext> uow,
            ILogger<RemoveChemicalImageCommandHandler> logger)
        {
            _chemicalRepository = chemicalRepository;
            _uow = uow;
            _logger = logger;
        }
        public async Task<ServiceResponse<string>> Handle(RemoveChemicalImageCommand request, CancellationToken cancellationToken)
        {
            var chemical = await _chemicalRepository.FindAsync(request.Id);
            if (chemical == null)
            {
                _logger.LogError("Chemical not found");
                return ServiceResponse<string>.Return404();
            }
            var chemicalUrl = chemical.Url;
            chemical.Url = string.Empty;
            _chemicalRepository.Update(chemical);
            if (await _uow.SaveAsync() <= 0)
            {
                _logger.LogError("Error while removing Chemical Image");
                return ServiceResponse<string>.Return500();
            }
            return ServiceResponse<string>.ReturnResultWith200(chemicalUrl);
        }
    }
}
