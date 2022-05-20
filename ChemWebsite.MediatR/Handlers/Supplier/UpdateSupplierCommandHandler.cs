using AutoMapper;
using ChemWebsite.Common.UnitOfWork;
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
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ChemWebsite.MediatR.Handlers
{
    public class UpdateSupplierCommandHandler : IRequestHandler<UpdateSupplierCommand, ServiceResponse<SupplierDto>>
    {
        private readonly ISupplierRepository _supplierRepository;
        private readonly IUnitOfWork<ChemWebsiteDbContext> _uow;
        private readonly ILogger<UpdateSupplierCommandHandler> _logger;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly PathHelper _pathHelper;

        public UpdateSupplierCommandHandler(ISupplierRepository supplierRepository,
            ILogger<UpdateSupplierCommandHandler> logger,
            IUnitOfWork<ChemWebsiteDbContext> uow,
            IMapper mapper,
              IWebHostEnvironment webHostEnvironment,
              PathHelper pathHelper
            )
        {
            _supplierRepository = supplierRepository;
            _uow = uow;
            _logger = logger;
            _mapper = mapper;
            _webHostEnvironment = webHostEnvironment;
            _pathHelper = pathHelper;
        }

        public async Task<ServiceResponse<SupplierDto>> Handle(UpdateSupplierCommand request, CancellationToken cancellationToken)
        {
            var entityExist = await _supplierRepository.FindBy(c => c.Id != request.Id && c.SupplierName == request.SupplierName.Trim())
                .FirstOrDefaultAsync();
            if (entityExist != null)
            {
                _logger.LogError("Supplier Name Already Exist for another supplier.");
                return ServiceResponse<SupplierDto>.Return422("Supplier Name Already Exist for another supplier.");
            }

            var entity = await _supplierRepository
              .FindByInclude(c => c.Id == request.Id, c => c.SupplierAddresses, c => c.SupplierEmails)
              .FirstOrDefaultAsync();

            if (request.IsImageUpload)
            {
                if (!string.IsNullOrEmpty(request.Logo))
                {
                    request.Url = Guid.NewGuid().ToString() + ".png";
                }
                else
                {
                    request.Url = "";
                }
            }
            else
            {
                request.Url = entity.Url;
            }

            var oldImageUrl = entity.Url;

            if (entity.SupplierEmails != null && request.SupplierEmails != null)
            {
                entity.SupplierEmails.ForEach(c =>
                {
                    if (!request.SupplierEmails.Any(se => se.Id == c.Id))
                    {
                        _uow.Context.SupplierEmails.Remove(c);
                    }
                });
            }
            if (entity.SupplierAddresses != null && request.SupplierAddresses != null)
            {
                entity.SupplierAddresses.ForEach(c =>
                {
                    if (!request.SupplierAddresses.Any(sa => sa.Id == c.Id))
                    {
                        _uow.Context.SupplierAddresses.Remove(c);
                    }
                });
            }

            entity = _mapper.Map(request, entity);
            _supplierRepository.Update(entity);
            if (_uow.Save() <= 0)
            {
                _logger.LogError("Error to Update Supplier");
                return ServiceResponse<SupplierDto>.Return500();
            }

            if (request.IsImageUpload)
            {
                string contentRootPath = _webHostEnvironment.WebRootPath;
                // delete old file
                if (!string.IsNullOrWhiteSpace(oldImageUrl)
                    && File.Exists(Path.Combine(contentRootPath, _pathHelper.SupplierImagePath, oldImageUrl)))
                {
                    FileData.DeleteFile(Path.Combine(contentRootPath, _pathHelper.SupplierImagePath, oldImageUrl));
                }
                // save new file
                if (!string.IsNullOrWhiteSpace(request.Logo))
                {
                    var pathToSave = Path.Combine(contentRootPath, _pathHelper.SupplierImagePath, entity.Url);
                    await FileData.SaveFile(pathToSave, request.Logo);
                }
            }
            return ServiceResponse<SupplierDto>.ReturnResultWith200(_mapper.Map<SupplierDto>(entity));
        }
    }
}
