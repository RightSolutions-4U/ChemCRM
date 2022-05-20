using AutoMapper;
using ChemWebsite.Common.UnitOfWork;
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
    public class UpdateInquiryStatusCommandHandler
        : IRequestHandler<UpdateInquiryStatusCommand, ServiceResponse<InquiryStatusDto>>
    {
        private readonly IInquiryStatusRepository _inquiryStatusRepository;
        private readonly IUnitOfWork<ChemWebsiteDbContext> _uow;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateInquiryStatusCommandHandler> _logger;
        public UpdateInquiryStatusCommandHandler(
           IInquiryStatusRepository inquiryStatusRepository,
            IMapper mapper,
            IUnitOfWork<ChemWebsiteDbContext> uow,
            ILogger<UpdateInquiryStatusCommandHandler> logger
            )
        {
            _inquiryStatusRepository = inquiryStatusRepository;
            _mapper = mapper;
            _uow = uow;
            _logger = logger;
        }
        public async Task<ServiceResponse<InquiryStatusDto>> Handle(UpdateInquiryStatusCommand request, CancellationToken cancellationToken)
        {
            var entityExist = await _inquiryStatusRepository.FindBy(c => c.Name == request.Name && c.Id != request.Id)
                .FirstOrDefaultAsync();
            if (entityExist != null)
            {
                _logger.LogError("Inquiry Status Already Exist.");
                return ServiceResponse<InquiryStatusDto>.Return409("Inquiry Status Already Exist.");
            }

            entityExist = await _inquiryStatusRepository.FindBy(v => v.Id == request.Id).FirstOrDefaultAsync();
            entityExist.Name = request.Name;
            _inquiryStatusRepository.Update(entityExist);

            if (await _uow.SaveAsync() <= 0)
            {
                return ServiceResponse<InquiryStatusDto>.Return500();
            }

            var entityDto = _mapper.Map<InquiryStatusDto>(entityExist);
            return ServiceResponse<InquiryStatusDto>.ReturnResultWith200(entityDto);
        }
    }
}
