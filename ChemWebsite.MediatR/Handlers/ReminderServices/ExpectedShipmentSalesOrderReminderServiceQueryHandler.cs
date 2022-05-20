using ChemWebsite.Common.UnitOfWork;
using ChemWebsite.Data.Entities;
using ChemWebsite.Domain;
using ChemWebsite.MediatR.CommandAndQuery;
using ChemWebsite.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ChemWebsite.MediatR.Handlers
{
   public class ExpectedShipmentSalesOrderReminderServiceQueryHandler : IRequestHandler<ExpectedShipmentSalesOrderReminderServiceQuery, bool>
    {
        private readonly ISalesOrderRepository _salesOrderRepository;
        private readonly IReminderSchedulerRepository _reminderSchedulerRepository;
        private readonly IUnitOfWork<ChemWebsiteDbContext> _uow;

        public ExpectedShipmentSalesOrderReminderServiceQueryHandler(
            ISalesOrderRepository salesOrderRepository,
            IReminderSchedulerRepository reminderSchedulerRepository,
            IUnitOfWork<ChemWebsiteDbContext> uow
            )
        {
            _salesOrderRepository = salesOrderRepository;
            _reminderSchedulerRepository = reminderSchedulerRepository;
            _uow = uow;
        }

        public async Task<bool> Handle(ExpectedShipmentSalesOrderReminderServiceQuery request, CancellationToken cancellationToken)
        {
            var currentToDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0);
            var currentFromDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 23, 59, 59);


            var salesOrders = await _salesOrderRepository
                .All
                .Where(c => !c.IsClosed
            && c.ExpectedShipmentDate >= currentToDate && c.ExpectedShipmentDate <= currentFromDate
           ).ToListAsync();

            if (salesOrders.Count() > 0)
            {
                var currentDate = DateTime.Now;
                List<ReminderScheduler> lstReminderScheduler = new();
                foreach (var salesOrder in salesOrders)
                {
                    var reminderScheduler = new ReminderScheduler
                    {
                        Frequency = Frequency.OneTime,
                        Application = ApplicationEnums.SalesOrder,
                        ReferenceId = salesOrder.Id,
                        CreatedDate = DateTime.Now,
                        IsActive = true,
                        Duration = new DateTime(salesOrder.ExpectedShipmentDate.Year, salesOrder.ExpectedShipmentDate.Month, salesOrder.ExpectedShipmentDate.Day, 0, 0, 0),
                        UserId = salesOrder.CreatedBy,
                        IsEmailNotification = true,
                        IsRead = false,
                        Subject = $"{salesOrder.SalesOrderNumber} ",
                        Message = $"{salesOrder.SalesOrderNumber} ",
                    };
                    lstReminderScheduler.Add(reminderScheduler);
                }
                _reminderSchedulerRepository.AddRange(lstReminderScheduler);
                if (await _uow.SaveAsync() <= 0)
                {
                    return false;
                }
            }
            return true;


        }
    }
}
