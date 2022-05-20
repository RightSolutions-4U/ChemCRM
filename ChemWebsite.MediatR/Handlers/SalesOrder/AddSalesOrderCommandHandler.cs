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
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ChemWebsite.MediatR.Handlers
{
    public class AddSalesOrderCommandHandler : IRequestHandler<AddSalesOrderCommand, ServiceResponse<SalesOrderDto>>
    {
        private readonly ISalesOrderRepository _salesOrderRepository;
        private readonly IChemicalCustomerRepository _chemicalCustomerRepository;
        private readonly IPurchaseOrderRepository _purchaseOrderRepository;
        private readonly ILogger<AddSalesOrderCommandHandler> _logger;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork<ChemWebsiteDbContext> _uow;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly PathHelper _pathHelper;

        public AddSalesOrderCommandHandler(ISalesOrderRepository salesOrderRepository,
            IChemicalCustomerRepository chemicalCustomerRepository,
            IPurchaseOrderRepository purchaseOrderRepository,
            ILogger<AddSalesOrderCommandHandler> logger,
            IMapper mapper,
            IUnitOfWork<ChemWebsiteDbContext> uow,
            IWebHostEnvironment webHostEnvironment,
            PathHelper pathHelper)
        {
            _salesOrderRepository = salesOrderRepository;
            _chemicalCustomerRepository = chemicalCustomerRepository;
            _purchaseOrderRepository = purchaseOrderRepository;
            _logger = logger;
            _mapper = mapper;
            _uow = uow;
            _webHostEnvironment = webHostEnvironment;
            _pathHelper = pathHelper;
        }

        public async Task<ServiceResponse<SalesOrderDto>> Handle(AddSalesOrderCommand request, CancellationToken cancellationToken)
        {
            var totalPOQuantity = request.SalesPurchaseOrderItems.Sum(c => c.Quantity);
            if (totalPOQuantity != request.Quantity)
            {
                _logger.LogError("Total Quanity of sales order does not matched with total quanity of purchase order");
                return ServiceResponse<SalesOrderDto>.ReturnFailed(400, "Total Quanity of sales order does not matched with total quanity of purchase order");
            }

            var isExistingSONumber = _salesOrderRepository.All.Any(c => c.SalesOrderNumber == request.SalesOrderNumber);
            if (isExistingSONumber)
            {
                _logger.LogError("Sales order number already exists.");
                return ServiceResponse<SalesOrderDto>.ReturnFailed(400, "Sales order number already exists.");
            }

            var allPOIds = request.SalesPurchaseOrderItems.Select(c => c.PurchaseOrderId).ToList();
            var allPO = await _purchaseOrderRepository.All.Where(c => allPOIds.Contains(c.Id)).ToListAsync();

            foreach (var po in allPO)
            {
                var poItem = request.SalesPurchaseOrderItems.FirstOrDefault(c => c.PurchaseOrderId == po.Id);
                po.InStockQuantity = po.InStockQuantity - poItem.Quantity;
                _purchaseOrderRepository.Update(po);
            }

            if (request.SalesOrderAttachments.Any())
            {
                string contentRootPath = _webHostEnvironment.WebRootPath;
                var pathToSave = Path.Combine(contentRootPath, _pathHelper.Attachments);

                if (!Directory.Exists(pathToSave))
                {
                    Directory.CreateDirectory(pathToSave);
                }

                foreach (var attachment in request.SalesOrderAttachments)
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
                        request.SalesOrderAttachments.Remove(attachment);
                    }
                }
            }

            var salesOrder = _mapper.Map<SalesOrder>(request);
            _salesOrderRepository.Add(salesOrder);

            // Chemical Customer
            var isExistingCustomer = _chemicalCustomerRepository.All.Any(c => c.ChemicalId == request.ChemicalId && c.CustomerId == request.CustomerId);
            if (!isExistingCustomer)
            {
                _chemicalCustomerRepository.Add(new ChemicalCustomer
                {
                    ChemicalId = request.ChemicalId,
                    CustomerId = request.CustomerId
                });
            }

            if (await _uow.SaveAsync() <= 0)
            {
                return ServiceResponse<SalesOrderDto>.Return500();
            }

            return ServiceResponse<SalesOrderDto>.ReturnResultWith201(_mapper.Map<SalesOrderDto>(salesOrder));
        }
    }
}
