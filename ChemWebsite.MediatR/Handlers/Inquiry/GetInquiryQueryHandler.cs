using AutoMapper;
using ChemWebsite.Common.UnitOfWork;
using ChemWebsite.Data.Dto;
using ChemWebsite.Domain;
using ChemWebsite.Helper;
using ChemWebsite.MediatR.CommandAndQuery;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ChemWebsite.MediatR.Handlers
{
    public class GetInquiryQueryHandler : IRequestHandler<GetInquiryQuery, ServiceResponse<InquiryDto>>
    {
        private readonly IUnitOfWork<ChemWebsiteDbContext> _uow;
        private readonly IMapper _mapper;
        private readonly ILogger<GetInquiryQueryHandler> _logger;
        public GetInquiryQueryHandler(IUnitOfWork<ChemWebsiteDbContext> uow,
            IMapper mapper,
            ILogger<GetInquiryQueryHandler> logger)
        {
            _uow = uow;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<ServiceResponse<InquiryDto>> Handle(GetInquiryQuery request, CancellationToken cancellationToken)
        {
            var inquiryEntity = await _uow.Context.Inquiries.Include(c => c.InquiryChemicals).ThenInclude(cs => cs.Chemical).FirstOrDefaultAsync(c => c.Id == request.Id);
            if (inquiryEntity == null)
            {
                _logger.LogError("Inquiry not found");
                return ServiceResponse<InquiryDto>.Return404();
            }
            var inquiryDto = _mapper.Map<InquiryDto>(inquiryEntity);
            inquiryDto.InquiryChemicals = inquiryEntity.InquiryChemicals.Select(c => new InquiryChemicalDto
            {
                ChemicalId = c.ChemicalId,
                Name = c.Chemical.Name,
                CasNumber = c.Chemical.CasNumber,
                InquiryId = c.InquiryId
            }).ToList();
            return ServiceResponse<InquiryDto>.ReturnResultWith200(inquiryDto);
        }
    }
}
