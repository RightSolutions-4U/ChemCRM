using ChemWebsite.Common.UnitOfWork;
using ChemWebsite.Data.Entities;
using ChemWebsite.Domain;
using ChemWebsite.MediatR.CommandAndQuery;
using ChemWebsite.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ChemWebsite.MediatR.Handlers
{
    public class CustomDateReminderServicesQueryHandler : IRequestHandler<CustomDateReminderServicesQuery, bool>
    {
        private readonly IReminderRepository _reminderRepository;
        private readonly IReminderSchedulerRepository _reminderSchedulerRepository;

        public CustomDateReminderServicesQueryHandler(IReminderRepository reminderRepository,
            IReminderSchedulerRepository reminderSchedulerRepository)
        {
            _reminderRepository = reminderRepository;
            _reminderSchedulerRepository = reminderSchedulerRepository;
        }

        public async Task<bool> Handle(CustomDateReminderServicesQuery request, CancellationToken cancellationToken)
        {
            var toDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0);
            var fromDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 23, 59, 59);

            var reminders = await _reminderRepository
                .All
                .Include(c => c.ReminderUsers)
                .Where(c => c.Frequency == Frequency.OneTime && !c.IsRepeated
                        && c.StartDate >= toDate
                        && c.StartDate <= fromDate)
                .ToListAsync();
            if (reminders.Count() > 0)
            {
                return await _reminderSchedulerRepository.AddMultiReminder(reminders);
            }
            return true;
        }
    }
}
