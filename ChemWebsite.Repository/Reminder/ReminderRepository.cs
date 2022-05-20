using ChemWebsite.Common.GenericRepository;
using ChemWebsite.Common.UnitOfWork;
using ChemWebsite.Data;
using ChemWebsite.Data.Dto;
using ChemWebsite.Data.Resources;
using ChemWebsite.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChemWebsite.Repository
{
    public class ReminderRepository : GenericRepository<Reminder, ChemWebsiteDbContext>,
        IReminderRepository
    {
        private readonly IPropertyMappingService _propertyMappingService;

        public ReminderRepository(
            IUnitOfWork<ChemWebsiteDbContext> uow,
            IPropertyMappingService propertyMappingService
            ) : base(uow)
        {
            _propertyMappingService = propertyMappingService;
        }

        public async Task<ReminderList> GetReminders(ReminderResource reminderResource)
        {
            var collectionBeforePaging = All;
            collectionBeforePaging =
               collectionBeforePaging.ApplySort(reminderResource.OrderBy,
               _propertyMappingService.GetPropertyMapping<ReminderDto, Reminder>());

            if (!string.IsNullOrWhiteSpace(reminderResource.Subject))
            {
                collectionBeforePaging = collectionBeforePaging
                    .Where(c => EF.Functions.Like(c.Subject, $"%{reminderResource.Subject}%"));
            }

            if (!string.IsNullOrWhiteSpace(reminderResource.Message))
            {
                collectionBeforePaging = collectionBeforePaging
                    .Where(c => EF.Functions.Like(c.Message, $"%{reminderResource.Message}%"));
            }

            if (reminderResource.Frequency.HasValue)
            {
                collectionBeforePaging = collectionBeforePaging
                    .Where(c => c.Frequency == reminderResource.Frequency);
            }

            var reminders = new ReminderList();
            return await reminders.Create(
                collectionBeforePaging,
                reminderResource.Skip,
                reminderResource.PageSize
                );
        }
    }
}
