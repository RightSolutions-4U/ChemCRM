using ChemWebsite.Data.Entities;
using ChemWebsite.Data.Resources;
using ChemWebsite.Helper;
using MediatR;

namespace ChemWebsite.MediatR.CommandAndQuery
{
    public class GetAllReminderNotificationQuery : IRequest<PagedList<ReminderScheduler>>
    {
        public ReminderResource ReminderResource { get; set; }
    }
}
