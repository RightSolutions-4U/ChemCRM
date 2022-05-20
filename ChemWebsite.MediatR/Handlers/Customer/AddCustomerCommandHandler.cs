using AutoMapper;
using ChemWebsite.Common.UnitOfWork;
using ChemWebsite.Data;
using ChemWebsite.Data.Dto;
using ChemWebsite.Domain;
using ChemWebsite.Helper;
using ChemWebsite.MediatR.CommandAndQuery;
using ChemWebsite.Repository;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace ChemWebsite.MediatR.Handlers
{
    public class AddCustomerCommandHandler : IRequestHandler<AddCustomerCommand, ServiceResponse<CustomerDto>>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork<ChemWebsiteDbContext> _uow;
        private readonly ILogger<AddCustomerCommandHandler> _logger;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly PathHelper _pathHelper;
        private readonly DashboardStatics _dashboardStatics;

        public AddCustomerCommandHandler(ICustomerRepository customerRepository,
            IMapper mapper,
            IUnitOfWork<ChemWebsiteDbContext> uow,
            ILogger<AddCustomerCommandHandler> logger,
            IWebHostEnvironment webHostEnvironment,
            PathHelper pathHelper,
            DashboardStatics dashboardStatics)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
            _uow = uow;
            _logger = logger;
            _webHostEnvironment = webHostEnvironment;
            _pathHelper = pathHelper;
            _dashboardStatics = dashboardStatics;
        }
        public async Task<ServiceResponse<CustomerDto>> Handle(AddCustomerCommand request, CancellationToken cancellationToken)
        {
            if (request.IsImageUpload && !string.IsNullOrEmpty(request.Logo))
            {
                var imageUrl = Guid.NewGuid().ToString() + ".png";
                request.Url = imageUrl;
            }

            var customerData = _mapper.Map<Customer>(request);
            _customerRepository.Add(customerData);

            if (await _uow.SaveAsync() <= 0)
            {
                _logger.LogError("Error while creating Customer.");
                return ServiceResponse<CustomerDto>.Return500();
            }
            var customerDtoData = _mapper.Map<CustomerDto>(customerData);

            if (request.IsImageUpload && !string.IsNullOrWhiteSpace(request.Url))
            {
                string contentRootPath = _webHostEnvironment.WebRootPath;
                var pathToSave = Path.Combine(contentRootPath, _pathHelper.CustomerImagePath, request.Url);
                await FileData.SaveFile(pathToSave, request.Logo);
            }
            _dashboardStatics.CustomerCount = _dashboardStatics.CustomerCount + 1;

            return ServiceResponse<CustomerDto>.ReturnResultWith200(customerDtoData);
        }
    }
}
