using ChemWebsite.Common.UnitOfWork;
using ChemWebsite.Data.Dto;
using ChemWebsite.Domain;
using ChemWebsite.Helper;
using ChemWebsite.MediatR.CommandAndQuery;
using ChemWebsite.Repository;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace ChemWebsite.MediatR.Handlers
{
    public class DeleteChemicalCommandHandler : IRequestHandler<DeleteChemicalCommand, ServiceResponse<ChemicalDto>>
    {

        private readonly IChemicalRepository _chemicalRepository;
        private readonly IUnitOfWork<ChemWebsiteDbContext> _uow;
        private readonly ILogger<DeleteChemicalCommandHandler> _logger;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly PathHelper _pathHelper;
        private readonly DashboardStatics _dashboardStatics;
        public DeleteChemicalCommandHandler(IChemicalRepository chemicalRepository,
            IUnitOfWork<ChemWebsiteDbContext> uow,
            ILogger<DeleteChemicalCommandHandler> logger,
            IWebHostEnvironment webHostEnvironment,
            PathHelper pathHelper,
            DashboardStatics dashboardStatics)
        {
            _chemicalRepository = chemicalRepository;
            _uow = uow;
            _logger = logger;
            _webHostEnvironment = webHostEnvironment;
            _pathHelper = pathHelper;
            _dashboardStatics = dashboardStatics;
        }
        public async Task<ServiceResponse<ChemicalDto>> Handle(DeleteChemicalCommand request, CancellationToken cancellationToken)
        {
            var chemical = await _chemicalRepository.FindAsync(request.Id);
            if (chemical == null)
            {
                _logger.LogError("Chemical not found.");
                return ServiceResponse<ChemicalDto>.Return404();
            }

            _chemicalRepository.Delete(chemical);

            if (await _uow.SaveAsync() <= 0)
            {
                _logger.LogError("Error while deleting the chemical.", request.Id);
                return ServiceResponse<ChemicalDto>.Return500();
            }

            if (!string.IsNullOrWhiteSpace(chemical.Url))
            {
                string contentRootPath = _webHostEnvironment.WebRootPath;
                var pathToSave = Path.Combine(contentRootPath, _pathHelper.ChemicalImagePath, chemical.Url);
                if (System.IO.File.Exists(pathToSave))
                {
                    System.IO.File.Delete(pathToSave);
                }
            }
            _dashboardStatics.ChemicalCount = _dashboardStatics.ChemicalCount - 1;
            return ServiceResponse<ChemicalDto>.ReturnSuccess();
        }
    }
}
