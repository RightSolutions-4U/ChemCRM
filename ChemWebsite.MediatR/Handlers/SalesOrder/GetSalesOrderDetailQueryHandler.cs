using ChemWebsite.Data.Dto;
using ChemWebsite.Helper;
using ChemWebsite.MediatR.CommandAndQuery;
using ChemWebsite.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ChemWebsite.MediatR.Handlers
{
    public class GetSalesOrderDetailQueryHandler
        : IRequestHandler<GetSalesOrderDetailQuery, ServiceResponse<SalesOrderDto>>
    {
        private readonly ISalesOrderRepository _salesOrderRepository;

        public GetSalesOrderDetailQueryHandler(ISalesOrderRepository salesOrderRepository)
        {
            _salesOrderRepository = salesOrderRepository;
        }

        public async Task<ServiceResponse<SalesOrderDto>> Handle(GetSalesOrderDetailQuery request, CancellationToken cancellationToken)
        {
            var salesOrder = await _salesOrderRepository
                .All
                .Include(cs => cs.Chemical)
                .Include(cs => cs.PaymentTerm)
                .Include(cs => cs.DeliveryMethod)
                .Include(cs => cs.Customer)
                .Include(cs => cs.SalesOrderAttachments)
                .Include(cs => cs.SalesPurchaseOrderItems)
                    .ThenInclude(y => y.PurchaseOrder)
                        .ThenInclude(c => c.Supplier)
                .Where(c => c.Id == request.Id)
                .FirstOrDefaultAsync();

            if (salesOrder == null)
            {
                return ServiceResponse<SalesOrderDto>.Return404();
            }

            var result = new SalesOrderDto
            {
                ChemicalName = salesOrder.Chemical.Name,
                ChemicalId = salesOrder.ChemicalId,
                CustomerId = salesOrder.CustomerId,
                CustomerName = salesOrder.Customer.CustomerName,
                CustomerNote = salesOrder.CustomerNote,
                ClosedDate = salesOrder.ClosedDate,
                DeliveryMethodId = salesOrder.DeliveryMethodId,
                DeliveryMethodName = salesOrder.DeliveryMethod?.Name,
                Discount = salesOrder.Discount,
                ExpectedShipmentDate = salesOrder.ExpectedShipmentDate,
                Id = salesOrder.Id,
                InvoiceGeneratedDate = salesOrder.InvoiceGeneratedDate,
                IsClosed = salesOrder.IsClosed,
                IsInvoiceGenerated = salesOrder.IsInvoiceGenerated,
                PaymentTermId = salesOrder.PaymentTermId,
                PaymentTermName = salesOrder.PaymentTerm?.Name,
                Quantity = salesOrder.Quantity,
                Rate = salesOrder.Rate,
                Reference = salesOrder.Reference,
                SalesOrderDate = salesOrder.SalesOrderDate,
                SalesOrderNumber = salesOrder.SalesOrderNumber,
                Tax = salesOrder.Tax,
                TermsAndConditions = salesOrder.TermsAndConditions,
                Total = salesOrder.Total,
                SalesPurchaseOrderItems = salesOrder.SalesPurchaseOrderItems.Select(c => new SalesPurchaseOrderItemDto
                {
                    SalesOrderId = c.SalesOrderId,
                    PurchaseOrderId = c.PurchaseOrderId,
                    Quantity = c.Quantity,
                    PurchaseOrderNumber = c.PurchaseOrder.OrderNumber,
                    SupplierName = c.PurchaseOrder.Supplier.SupplierName,
                    PricePerUnit = c.PurchaseOrder.PricePerUnit
                }).ToList(),
                SalesOrderAttachments = salesOrder.SalesOrderAttachments.Select(c => new SalesOrderAttachmentDto
                {
                    Id = c.Id,
                    Name = c.Name,
                    CreatedDate = c.CreatedDate
                }).ToList()
            };

            return ServiceResponse<SalesOrderDto>.ReturnResultWith200(result);
        }
    }
}
