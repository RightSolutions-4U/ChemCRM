﻿using AutoMapper;
using ChemWebsite.Common.UnitOfWork;
using ChemWebsite.Data;
using ChemWebsite.Data.Dto;
using ChemWebsite.Domain;
using ChemWebsite.Helper;
using ChemWebsite.MediatR.CommandAndQuery;
using ChemWebsite.Repository;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ChemWebsite.MediatR.Handlers
{
    public class AddReminderCommandHandler
        : IRequestHandler<AddReminderCommand, ServiceResponse<ReminderDto>>
    {

        private readonly IReminderRepository _reminderRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork<ChemWebsiteDbContext> _uow;
        private readonly UserInfoToken _userInfoToken;

        public AddReminderCommandHandler(IReminderRepository reminderRepository,
            IMapper mapper,
            IUnitOfWork<ChemWebsiteDbContext> uow,
            UserInfoToken userInfoToken)
        {
            _reminderRepository = reminderRepository;
            _mapper = mapper;
            _uow = uow;
            _userInfoToken = userInfoToken;
        }

        public async Task<ServiceResponse<ReminderDto>> Handle(AddReminderCommand request, CancellationToken cancellationToken)
        {

            if (!request.ReminderUsers.Any(c => c.UserId.ToString() == _userInfoToken.Id))
            {
                request.ReminderUsers.Add(new ReminderUserDto
                {
                    UserId = Guid.Parse(_userInfoToken.Id)
                });
            }
            var reminder = _mapper.Map<Reminder>(request);
            _reminderRepository.Add(reminder);
            if (await _uow.SaveAsync() <= 0)
            {
                return ServiceResponse<ReminderDto>.Return500();
            }

            return ServiceResponse<ReminderDto>.ReturnResultWith201(_mapper.Map<ReminderDto>(reminder));
        }
    }
}
