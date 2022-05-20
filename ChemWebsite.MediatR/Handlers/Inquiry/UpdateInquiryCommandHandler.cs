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
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ChemWebsite.MediatR.Handlers
{
    public class UpdateInquiryCommandHandler : IRequestHandler<UpdateInquiryCommand, ServiceResponse<InquiryDto>>
    {
        private readonly IInquiryRepository _inquiryRepository;
        private readonly IInquiryChemicalRepository _inquiryChemicalRepository;
        private readonly IUnitOfWork<ChemWebsiteDbContext> _uow;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateInquiryCommandHandler> _logger;

        public UpdateInquiryCommandHandler(
            IInquiryRepository inquiryRepository,
            IUnitOfWork<ChemWebsiteDbContext> uow,
            IMapper mapper,
            IInquiryChemicalRepository inquiryChemicalRepository,
            ILogger<UpdateInquiryCommandHandler> logger)
        {
            _inquiryRepository = inquiryRepository;
            _uow = uow;
            _mapper = mapper;
            _inquiryChemicalRepository = inquiryChemicalRepository;
            _logger = logger;
        }

        public async Task<ServiceResponse<InquiryDto>> Handle(UpdateInquiryCommand request, CancellationToken cancellationToken)
        {
            request.InquiryChemicals = request.InquiryChemicals.DistinctBy(c => c.ChemicalId).ToList();
            var entityExist = await _inquiryRepository.AllIncluding(c => c.InquiryChemicals).Where(c => c.Id == request.Id).FirstOrDefaultAsync();
            if (entityExist == null)
            {
                _logger.LogError("Inquiry does not exists.");
                return ServiceResponse<InquiryDto>.Return409("Inquiry does not exists.");
            }

            if (entityExist.InquiryChemicals != null && request.InquiryChemicals != null)
            {
                entityExist.InquiryChemicals.ForEach(c =>
                {
                    if (!request.InquiryChemicals.Any(se => se.ChemicalId == c.ChemicalId && se.InquiryId == c.InquiryId))
                    {
                        _uow.Context.InquiryChemicals.Remove(c);
                    }
                });
            }
            _mapper.Map(request, entityExist);
            _inquiryRepository.Update(entityExist);
            if (await _uow.SaveAsync() <= 0)
            {
                return ServiceResponse<InquiryDto>.Return500();
            }
            var industrydto = _mapper.Map<InquiryDto>(entityExist);
            return ServiceResponse<InquiryDto>.ReturnResultWith200(industrydto);
        }
    }
}
