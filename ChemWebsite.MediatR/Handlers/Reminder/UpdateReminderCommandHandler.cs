using AutoMapper;
using ChemWebsite.Common.UnitOfWork;
using ChemWebsite.Data;
using ChemWebsite.Data.Dto;
using ChemWebsite.Domain;
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
    public class UpdateReminderCommandHandler
      : IRequestHandler<UpdateReminderCommand, ServiceResponse<ReminderDto>>
    {

        private readonly IReminderRepository _reminderRepository;
        private readonly IReminderUserRepository _reminderUserRepository;
        private readonly IDailyReminderRepository _dailyReminderRepository;
        private readonly IQuarterlyReminderRepository _quarterlyReminderRepository;
        private readonly IHalfYearlyReminderRepository _halfYearlyReminderRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork<ChemWebsiteDbContext> _uow;

        public UpdateReminderCommandHandler(IReminderRepository reminderRepository,
            IReminderUserRepository reminderUserRepository,
            IDailyReminderRepository dailyReminderRepository,
            IQuarterlyReminderRepository quarterlyReminderRepository,
            IHalfYearlyReminderRepository halfYearlyReminderRepository,
            IMapper mapper,
            IUnitOfWork<ChemWebsiteDbContext> uow)
        {
            _reminderRepository = reminderRepository;
            _reminderUserRepository = reminderUserRepository;
            _dailyReminderRepository = dailyReminderRepository;
            _quarterlyReminderRepository = quarterlyReminderRepository;
            _halfYearlyReminderRepository = halfYearlyReminderRepository;
            _mapper = mapper;
            _uow = uow;
        }

        public async Task<ServiceResponse<ReminderDto>> Handle(UpdateReminderCommand request, CancellationToken cancellationToken)
        {
            var entity = await _reminderRepository
                .AllIncluding(c => c.DailyReminders, cs => cs.QuarterlyReminders, h => h.HalfYearlyReminders, u => u.ReminderUsers).FirstOrDefaultAsync(c => c.Id == request.Id);
            if (entity == null)
            {
                return ServiceResponse<ReminderDto>.Return404();
            }

            _reminderUserRepository.RemoveRange(entity.ReminderUsers);
            _dailyReminderRepository.RemoveRange(entity.DailyReminders);
            _quarterlyReminderRepository.RemoveRange(entity.QuarterlyReminders);
            _halfYearlyReminderRepository.RemoveRange(entity.HalfYearlyReminders);

            var reminder = _mapper.Map(request, entity);
            if (!request.IsRepeated)
            {
                reminder.Frequency = null;
                reminder.DayOfWeek = null;
            }
            _reminderRepository.Update(reminder);

            if (await _uow.SaveAsync() <= 0)
            {
                return ServiceResponse<ReminderDto>.Return500();
            }

            return ServiceResponse<ReminderDto>.ReturnResultWith201(_mapper.Map<ReminderDto>(reminder));
        }
    }
}
