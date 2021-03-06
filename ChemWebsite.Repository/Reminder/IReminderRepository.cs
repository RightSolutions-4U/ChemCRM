using ChemWebsite.Common.GenericRepository;
using ChemWebsite.Data;
using ChemWebsite.Data.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChemWebsite.Repository
{
    public interface IReminderRepository : IGenericRepository<Reminder>
    {
        Task<ReminderList> GetReminders(ReminderResource reminderResource);
    }
}
