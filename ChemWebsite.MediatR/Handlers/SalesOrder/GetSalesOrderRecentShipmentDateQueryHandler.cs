using ChemWebsite.Data.Dto;
using ChemWebsite.MediatR.CommandAndQuery;
using ChemWebsite.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ChemWebsite.MediatR.Handlers
{
    public class GetSalesOrderRecentShipmentDateQueryHandler : IRequestHandler<GetSalesOrderRecentShipmentDateQuery, List<SalesOrderRecentShipmentDate>>
    {

        private readonly ISalesOrderRepository _salesOrderRepository;

        public GetSalesOrderRecentShipmentDateQueryHandler(
            ISalesOrderRepository salesOrderRepository
          )
        {
            _salesOrderRepository = salesOrderRepository;
        }

        public async Task<List<SalesOrderRecentShipmentDate>> Handle(GetSalesOrderRecentShipmentDateQuery request, CancellationToken cancellationToken)
        {
            var entities = await _salesOrderRepository.All
                        .Include(c => c.Chemical)
                        .Include(c=> c.Customer)
                         .Where(c => !c.IsClosed)
                         .OrderByDescending(c => c.ExpectedShipmentDate)
                         .Take(10)
                         .Select(c => new SalesOrderRecentShipmentDate
                         {
                             SalesOrderId = c.Id,
                             SalesOrderNumber = c.SalesOrderNumber,
                             ExpectedShipmentDate = c.ExpectedShipmentDate,
                             Quantity = c.Quantity,
                             CustomerId = c.CustomerId,
                             CustomerName = c.Customer.CustomerName,
                             ChemicalId = c.ChemicalId,
                             ChemicalName = c.Chemical.Name
                         })
                     .ToListAsync();

            return entities;
        }
    }

    }
