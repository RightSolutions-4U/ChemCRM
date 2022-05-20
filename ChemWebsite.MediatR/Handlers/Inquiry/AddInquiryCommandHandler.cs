using AutoMapper;
using ChemWebsite.Common.UnitOfWork;
using ChemWebsite.Data;
using ChemWebsite.Data.Dto;
using ChemWebsite.Domain;
using ChemWebsite.Helper;
using ChemWebsite.MediatR.CommandAndQuery;
using ChemWebsite.Repository;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ChemWebsite.MediatR.Handlers
{
    public class AddInquiryCommandHandler : IRequestHandler<AddInquiryCommand, ServiceResponse<InquiryDto>>
    {

        private readonly IInquiryRepository _inquiryRepository;
        private readonly IUnitOfWork<ChemWebsiteDbContext> _uow;
        private readonly IMapper _mapper;
        private readonly DashboardStatics _dashboardStatics;

        public AddInquiryCommandHandler(
            IInquiryRepository inquiryRepository,
            IUnitOfWork<ChemWebsiteDbContext> uow,
            IMapper mapper,
            DashboardStatics dashboardStatics)
        {
            _inquiryRepository = inquiryRepository;
            _uow = uow;
            _mapper = mapper;
            _dashboardStatics = dashboardStatics;
        }

        public async Task<ServiceResponse<InquiryDto>> Handle(AddInquiryCommand request, CancellationToken cancellationToken)
        {
            request.InquiryChemicals = request.InquiryChemicals.DistinctBy(c => c.ChemicalId).ToList();
            var entity = _mapper.Map<Inquiry>(request);
            _inquiryRepository.Add(entity);
            if (await _uow.SaveAsync() <= 0)
            {
                return ServiceResponse<InquiryDto>.Return500();
            }
            var industrydto = _mapper.Map<InquiryDto>(entity);
            _dashboardStatics.InquiryCount = _dashboardStatics.InquiryCount + 1;
            return ServiceResponse<InquiryDto>.ReturnResultWith200(industrydto);
        }


    }
}
