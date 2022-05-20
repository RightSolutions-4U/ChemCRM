﻿using ChemWebsite.Common.UnitOfWork;
using ChemWebsite.Domain;
using ChemWebsite.Helper;
using ChemWebsite.MediatR.CommandAndQuery;
using ChemWebsite.Repository;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ChemWebsite.MediatR.Handlers
{
    public class DeleteReminderCommandHandler : IRequestHandler<DeleteReminderCommand, ServiceResponse<bool>>
    {
        private readonly IReminderRepository _reminderRepository;
        private readonly IUnitOfWork<ChemWebsiteDbContext> _uow;

        public DeleteReminderCommandHandler(IReminderRepository reminderRepository,
            IUnitOfWork<ChemWebsiteDbContext> uow)
        {
            _reminderRepository = reminderRepository;
            _uow = uow;
        }

        public async Task<ServiceResponse<bool>> Handle(DeleteReminderCommand request, CancellationToken cancellationToken)
        {
            var reminder = await _reminderRepository.FindAsync(request.Id);
            if (reminder == null)
            {
                return ServiceResponse<bool>.Return404();
            }

            reminder.IsDeleted = true;
            _reminderRepository.Update(reminder);
            if (await _uow.SaveAsync() <= 0)
            {
                return ServiceResponse<bool>.Return500();
            }

            return ServiceResponse<bool>.ReturnSuccess();
        }
    }
}
