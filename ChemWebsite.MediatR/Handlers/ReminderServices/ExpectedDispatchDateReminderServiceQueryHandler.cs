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
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ChemWebsite.MediatR.Handlers
{
    public class ExpectedDispatchDateReminderServiceQueryHandler : IRequestHandler<ExpectedDispatchDateReminderServiceQuery, bool>
    {
        private readonly IPurchaseOrderDeliveryScheduleRepository _purchaseOrderDeliveryScheduleRepository;
        private readonly IReminderSchedulerRepository _reminderSchedulerRepository;
        private readonly IUnitOfWork<ChemWebsiteDbContext> _uow;

        public ExpectedDispatchDateReminderServiceQueryHandler(
            IPurchaseOrderDeliveryScheduleRepository purchaseOrderDeliveryScheduleRepository,
            IReminderSchedulerRepository reminderSchedulerRepository,
            IUnitOfWork<ChemWebsiteDbContext> uow
            )
        {
            _purchaseOrderDeliveryScheduleRepository = purchaseOrderDeliveryScheduleRepository;
            _reminderSchedulerRepository = reminderSchedulerRepository;
            _uow = uow;
        }

        public async Task<bool> Handle(ExpectedDispatchDateReminderServiceQuery request, CancellationToken cancellationToken)
        {
            var currentToDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day,0,0,0);
            var currentFromDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 23, 59, 59);
           

            var purchaseOrderDeliverySchedules = await _purchaseOrderDeliveryScheduleRepository
                .All
                .Include(c => c.PurchaseOrder)
                .Where(c => !c.IsReceived
            && c.ExpectedDispatchDate >= currentToDate && c.ExpectedDispatchDate <= currentFromDate
           ).ToListAsync();

            if (purchaseOrderDeliverySchedules.Count() > 0)
            {
                var currentDate = DateTime.Now;
                List<ReminderScheduler> lstReminderScheduler = new();
                foreach (var purchaseOrderDeliverySchedule in purchaseOrderDeliverySchedules)
                {
                   
                        var reminderScheduler = new ReminderScheduler
                        {
                            Frequency = Frequency.OneTime,
                            Application= ApplicationEnums.PurchaseOrder,
                            ReferenceId= purchaseOrderDeliverySchedule.PurchaseOrder.Id,
                            CreatedDate = DateTime.Now,
                            IsActive = true,
                            Duration = new DateTime(purchaseOrderDeliverySchedule.ExpectedDispatchDate.Year, purchaseOrderDeliverySchedule.ExpectedDispatchDate.Month, purchaseOrderDeliverySchedule.ExpectedDispatchDate.Day, 0, 0, 0),
                            UserId = purchaseOrderDeliverySchedule.PurchaseOrder.CreatedBy,
                            IsEmailNotification = true,
                            IsRead = false,
                            Subject = $"{purchaseOrderDeliverySchedule.PurchaseOrder.OrderNumber} ",
                            Message = $"{purchaseOrderDeliverySchedule.PurchaseOrder.OrderNumber} ",
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
