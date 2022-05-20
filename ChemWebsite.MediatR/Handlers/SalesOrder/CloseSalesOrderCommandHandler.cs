using ChemWebsite.Common.UnitOfWork;
using ChemWebsite.Domain;
using ChemWebsite.Helper;
using ChemWebsite.MediatR.CommandAndQuery;
using ChemWebsite.Repository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ChemWebsite.MediatR.Handlers
{
    public class CloseSalesOrderCommandHandler : IRequestHandler<CloseSalesOrderCommand, ServiceResponse<bool>>
    {
        private readonly ISalesOrderRepository _salesOrderRepository;
        private readonly IUnitOfWork<ChemWebsiteDbContext> _uow;

        public CloseSalesOrderCommandHandler(ISalesOrderRepository salesOrderRepository,
            IUnitOfWork<ChemWebsiteDbContext> uow)
        {
            _salesOrderRepository = salesOrderRepository;
            _uow = uow;
        }
        public async Task<ServiceResponse<bool>> Handle(CloseSalesOrderCommand request, CancellationToken cancellationToken)
        {
            var salesOrder = await _salesOrderRepository.FindAsync(request.Id);
            if (salesOrder == null)
            {
                return ServiceResponse<bool>.Return404();
            }
            salesOrder.IsClosed = true;
            salesOrder.ClosedDate = DateTime.Now;

            _salesOrderRepository.Update(salesOrder);
            if (await _uow.SaveAsync() <= 0)
            {
                return ServiceResponse<bool>.Return500();
            }

            return ServiceResponse<bool>.ReturnSuccess();
        }
    }
}
