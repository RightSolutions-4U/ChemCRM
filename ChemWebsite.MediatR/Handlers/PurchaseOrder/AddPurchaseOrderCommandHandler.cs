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
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ChemWebsite.MediatR.Handlers
{
    public class AddPurchaseOrderCommandHandler : IRequestHandler<AddPurchaseOrderCommand, ServiceResponse<PurchaseOrderDto>>
    {
        private readonly IPurchaseOrderRepository _purchaseOrderRepository;
        private readonly ISupplierChemicalRepository _supplierChemicalRepository;
        private readonly IUnitOfWork<ChemWebsiteDbContext> _uow;
        private readonly IMapper _mapper;
        private readonly ILogger<AddPurchaseOrderCommandHandler> _logger;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly PathHelper _pathHelper;

        public AddPurchaseOrderCommandHandler(
            IPurchaseOrderRepository purchaseOrderRepository,
            ISupplierChemicalRepository supplierChemicalRepository,
            IUnitOfWork<ChemWebsiteDbContext> uow,
            IMapper mapper,
            ILogger<AddPurchaseOrderCommandHandler> logger,
            IWebHostEnvironment webHostEnvironment,
            PathHelper pathHelper)
        {
            _purchaseOrderRepository = purchaseOrderRepository;
            _supplierChemicalRepository = supplierChemicalRepository;
            _uow = uow;
            _mapper = mapper;
            _logger = logger;
            _webHostEnvironment = webHostEnvironment;
            _pathHelper = pathHelper;
        }

        public async Task<ServiceResponse<PurchaseOrderDto>> Handle(AddPurchaseOrderCommand request, CancellationToken cancellationToken)
        {

            var existingPONumber = _purchaseOrderRepository.All.Any(c => c.OrderNumber == request.OrderNumber);
            if (existingPONumber)
            {
                return ServiceResponse<PurchaseOrderDto>.Return409("Purchase Order Number is already Exists.");
            }

            if (request.PurchaseOrderAttachments.Any())
            {
                string contentRootPath = _webHostEnvironment.WebRootPath;
                var pathToSave = Path.Combine(contentRootPath, _pathHelper.Attachments);

                if (!Directory.Exists(pathToSave))
                {
                    Directory.CreateDirectory(pathToSave);
                }

                foreach (var attachment in request.PurchaseOrderAttachments)
                {
                    var extension = Path.GetExtension(attachment.Name); ;
                    var id = Guid.NewGuid();
                    var path = $"{id}.{extension}";
                    var documentPath = Path.Combine(pathToSave, path);
                    string base64 = attachment.DocumentData.Split(',').LastOrDefault();
                    if (!string.IsNullOrWhiteSpace(base64))
                    {
                        byte[] bytes = Convert.FromBase64String(base64);
                        try
                        {
                            await File.WriteAllBytesAsync($"{documentPath}", bytes);
                            attachment.Path = path;
                        }
                        catch
                        {
                            _logger.LogError("Error while saving files", attachment);
                        }
                    }
                    else
                    {
                        request.PurchaseOrderAttachments.Remove(attachment);
                    }
                }
            }

            var purchaseOrder = _mapper.Map<PurchaseOrder>(request);
            var availableQuntity = purchaseOrder.PurchaseOrderDeliverySchedules.Where(c => c.IsReceived).Sum(c => c.ExpectedDispatchQuantity);
            purchaseOrder.AvailableQuantity = availableQuntity;
            purchaseOrder.InStockQuantity = purchaseOrder.TotalQuantity;
            if (purchaseOrder.AvailableQuantity == purchaseOrder.TotalQuantity)
            {
                purchaseOrder.IsClosed = true;
                purchaseOrder.ClosedDate = DateTime.Now;
            }

            _purchaseOrderRepository.Add(purchaseOrder);

            // Chemical Supplier
            var isSupplierExisting = _supplierChemicalRepository.All.Any(c => c.ChemicalId == request.ChemicalId && c.SupplierId == request.SupplierId);
            if (!isSupplierExisting)
            {
                _supplierChemicalRepository.Add(new ChemicalSupplier
                {
                    ChemicalId = request.ChemicalId,
                    SupplierId = request.SupplierId
                });
            }

            if (await _uow.SaveAsync() <= 0)
            {
                _logger.LogError("Error while creating Purchase Order.");
                return ServiceResponse<PurchaseOrderDto>.Return500();
            }
            var dto = _mapper.Map<PurchaseOrderDto>(purchaseOrder);
            return ServiceResponse<PurchaseOrderDto>.ReturnResultWith201(dto);
        }
    }
}
