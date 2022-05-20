﻿using AutoMapper;
using ChemWebsite.Common.UnitOfWork;
using ChemWebsite.Data;
using ChemWebsite.Data.Dto;
using ChemWebsite.Domain;
using ChemWebsite.Helper;
using ChemWebsite.MediatR.CommandAndQuery;
using ChemWebsite.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ChemWebsite.MediatR.Handlers
{
    public class AddInquirySourceCommandHandler
        : IRequestHandler<AddInquirySourceCommand, ServiceResponse<InquirySourceDto>>
    {
        private readonly IInquirySourceRepository _inquirySourceRepository;
        private readonly IUnitOfWork<ChemWebsiteDbContext> _uow;
        private readonly IMapper _mapper;
        private readonly ILogger<AddInquirySourceCommandHandler> _logger;
        public AddInquirySourceCommandHandler(
           IInquirySourceRepository inquirySourceRepository,
            IMapper mapper,
            IUnitOfWork<ChemWebsiteDbContext> uow,
            ILogger<AddInquirySourceCommandHandler> logger
            )
        {
            _inquirySourceRepository = inquirySourceRepository;
            _mapper = mapper;
            _uow = uow;
            _logger = logger;
        }
        public async Task<ServiceResponse<InquirySourceDto>> Handle(AddInquirySourceCommand request, CancellationToken cancellationToken)
        {
            var existingEntity = await _inquirySourceRepository.FindBy(c => c.Name == request.Name).FirstOrDefaultAsync();
            if (existingEntity != null)
            {
                _logger.LogError("Inquiry Source Already Exist");
                return ServiceResponse<InquirySourceDto>.Return409("Inquiry Source Already Exist.");
            }
            var entity = _mapper.Map<InquirySource>(request);
            entity.Id = Guid.NewGuid();
            _inquirySourceRepository.Add(entity);
            if (await _uow.SaveAsync() <= 0)
            {

                _logger.LogError("Error While saving Inquiry Source.");
                return ServiceResponse<InquirySourceDto>.Return500();
            }
            return ServiceResponse<InquirySourceDto>.ReturnResultWith200(_mapper.Map<InquirySourceDto>(entity));
        }
    }
}
