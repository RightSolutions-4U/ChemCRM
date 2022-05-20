using AutoMapper;
using ChemWebsite.Common.UnitOfWork;
using ChemWebsite.Data;
using ChemWebsite.Data.Dto;
using ChemWebsite.Domain;
using ChemWebsite.Helper;
using ChemWebsite.MediatR.CommandAndQuery;
using ChemWebsite.Repository;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace ChemWebsite.MediatR.Handlers
{
    public class AddSupplierCommandHandler : IRequestHandler<AddSupplierCommand, ServiceResponse<SupplierDto>>
    {
        private readonly ISupplierRepository _supplierRepository;
        private readonly IUnitOfWork<ChemWebsiteDbContext> _uow;
        private readonly ILogger<AddSupplierCommandHandler> _logger;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly PathHelper _pathHelper;
        private readonly DashboardStatics _dashboardStatics;

        public AddSupplierCommandHandler(ISupplierRepository supplierRepository,
            ILogger<AddSupplierCommandHandler> logger,
            IUnitOfWork<ChemWebsiteDbContext> uow,
            IMapper mapper,
              IWebHostEnvironment webHostEnvironment,
              PathHelper pathHelper,
              DashboardStatics dashboardStatics
            )
        {
            _supplierRepository = supplierRepository;
            _uow = uow;
            _logger = logger;
            _mapper = mapper;
            _webHostEnvironment = webHostEnvironment;
            _pathHelper = pathHelper;
            _dashboardStatics = dashboardStatics;
        }

        public async Task<ServiceResponse<SupplierDto>> Handle(AddSupplierCommand request, CancellationToken cancellationToken)
        {
            if (request.IsImageUpload && !string.IsNullOrEmpty(request.Logo))
            {
                var imageUrl = Guid.NewGuid().ToString() + ".png";
                request.Url = imageUrl;
            }

            var entity = await _supplierRepository.FindBy(c => c.SupplierName == request.SupplierName).FirstOrDefaultAsync();
            if (entity != null)
            {
                _logger.LogError("Supplier Name is already exist.");
                return ServiceResponse<SupplierDto>.Return422("Supplier Name is already exist.");
            }
            entity = _mapper.Map<Supplier>(request);
            _supplierRepository.Add(entity);

            if (await _uow.SaveAsync() <= 0)
            {
                _logger.LogError("Error to Save Supplier");
                return ServiceResponse<SupplierDto>.Return500();
            }

            if (request.IsImageUpload && !string.IsNullOrWhiteSpace(entity.Url))
            {
                string contentRootPath = _webHostEnvironment.WebRootPath;
                var pathToSave = Path.Combine(contentRootPath, _pathHelper.SupplierImagePath, entity.Url);
                await FileData.SaveFile(pathToSave, request.Logo);
            }
            _dashboardStatics.SupplierCount = _dashboardStatics.SupplierCount + 1;
            return ServiceResponse<SupplierDto>.ReturnResultWith200(_mapper.Map<SupplierDto>(entity));
        }
    }
}
