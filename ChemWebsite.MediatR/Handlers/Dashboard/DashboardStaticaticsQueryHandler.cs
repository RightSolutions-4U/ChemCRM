using ChemWebsite.Data.Dto;
using ChemWebsite.MediatR.CommandAndQuery;
using ChemWebsite.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace ChemWebsite.MediatR.Handlers
{
    public class DashboardStaticaticsQueryHandler : IRequestHandler<DashboardStaticaticsQuery, DashboardStatics>
    {
        private readonly IInquiryRepository _inquiryRepository;
        private readonly DashboardStatics _dashboardStatics;
        private readonly ISupplierRepository _supplierRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IChemicalRepository _chemicalRepository;

        public DashboardStaticaticsQueryHandler(
            IInquiryRepository inquiryRepository,
            DashboardStatics dashboardStatics,
            ISupplierRepository supplierRepository,
            ICustomerRepository customerRepository,
            IChemicalRepository chemicalRepository)
        {
            _inquiryRepository = inquiryRepository;
            _dashboardStatics = dashboardStatics;
            _supplierRepository = supplierRepository;
            _customerRepository = customerRepository;
            _chemicalRepository = chemicalRepository;
        }
        public async Task<DashboardStatics> Handle(DashboardStaticaticsQuery request, CancellationToken cancellationToken)
        {
            if (_dashboardStatics.ChemicalCount == 0)
            {
                _dashboardStatics.InquiryCount = await _inquiryRepository.All.CountAsync();
                _dashboardStatics.SupplierCount = await _supplierRepository.All.CountAsync();
                _dashboardStatics.ChemicalCount = await _chemicalRepository.All.CountAsync();
                _dashboardStatics.CustomerCount = await _customerRepository.All.CountAsync();
            }
            return _dashboardStatics;
        }
    }
}
