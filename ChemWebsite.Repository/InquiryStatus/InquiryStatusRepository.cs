using AutoMapper;
using ChemWebsite.Common.GenericRepository;
using ChemWebsite.Common.UnitOfWork;
using ChemWebsite.Data.Entities;
using ChemWebsite.Domain;

namespace ChemWebsite.Repository
{
    public class InquiryStatusRepository : GenericRepository<InquiryStatus, ChemWebsiteDbContext>, IInquiryStatusRepository
    {
        private readonly IPropertyMappingService _propertyMappingService;
        private readonly IMapper _mapper;
        public InquiryStatusRepository(
            IUnitOfWork<ChemWebsiteDbContext> uow,
            IPropertyMappingService propertyMappingService,
             IMapper mapper)
            : base(uow)
        {
            _mapper = mapper;
            _propertyMappingService = propertyMappingService;
        }
    }
}
