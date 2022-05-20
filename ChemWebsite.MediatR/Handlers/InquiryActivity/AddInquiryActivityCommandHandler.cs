using AutoMapper;
using ChemWebsite.Common.UnitOfWork;
using ChemWebsite.Data.Dto;
using ChemWebsite.Data.Entities;
using ChemWebsite.Domain;
using ChemWebsite.Helper;
using ChemWebsite.MediatR.CommandAndQuery;
using ChemWebsite.Repository;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace ChemWebsite.MediatR.Handlers
{
    public class AddInquiryActivityCommandHandler : IRequestHandler<AddInquiryActivityCommand, ServiceResponse<InquiryActivityDto>>
    {
        private readonly IInquiryActivityRepository _inquiryActivityRepository;
        private readonly IUnitOfWork<ChemWebsiteDbContext> _uow;
        private readonly IMapper _mapper;
        private readonly ILogger<AddInquiryActivityCommandHandler> _logger;

        public AddInquiryActivityCommandHandler(
            IInquiryActivityRepository inquiryActivityRepository,
            IUnitOfWork<ChemWebsiteDbContext> uow,
            IMapper mapper,
            ILogger<AddInquiryActivityCommandHandler> logger)
        {
            _inquiryActivityRepository = inquiryActivityRepository;
            _uow = uow;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<ServiceResponse<InquiryActivityDto>> Handle(AddInquiryActivityCommand request, CancellationToken cancellationToken)
        {
            var inquiryActivityEntity = _mapper.Map<InquiryActivity>(request);
            _inquiryActivityRepository.Add(inquiryActivityEntity);
            if (await _uow.SaveAsync() <= 0)
            {
                _logger.LogError("Error while adding industry");
                return ServiceResponse<InquiryActivityDto>.Return500();
            }
            var dto = _mapper.Map<InquiryActivityDto>(inquiryActivityEntity);
            return ServiceResponse<InquiryActivityDto>.ReturnResultWith200(dto);
        }
    }
}
