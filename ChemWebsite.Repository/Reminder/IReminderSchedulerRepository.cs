using ChemWebsite.Common.GenericRepository;
using ChemWebsite.Data;
using ChemWebsite.Data.Entities;
using ChemWebsite.Data.Resources;
using ChemWebsite.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChemWebsite.Repository
{
   public interface IReminderSchedulerRepository : IGenericRepository<ReminderScheduler>
    {
        Task<bool> AddMultiReminder(List<Reminder> reminders);
        Task<PagedList<ReminderScheduler>> GetReminders(ReminderResource reminderResource);
        Task<bool> MarkAsRead();
    }
}
