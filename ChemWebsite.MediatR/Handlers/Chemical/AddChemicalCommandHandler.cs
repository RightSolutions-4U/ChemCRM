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
    public class AddChemicalCommandHandler : IRequestHandler<AddChemicalCommand, ServiceResponse<ChemicalDto>>
    {
        private readonly IChemicalRepository _chemicalRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork<ChemWebsiteDbContext> _uow;
        private readonly ILogger<AddChemicalCommandHandler> _logger;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly PathHelper _pathHelper;
        private readonly DashboardStatics _dashboardStatics;
        public AddChemicalCommandHandler(IChemicalRepository chemicalRepository,
            IMapper mapper,
            IUnitOfWork<ChemWebsiteDbContext> uow,
            ILogger<AddChemicalCommandHandler> logger,
             IWebHostEnvironment webHostEnvironment,
             PathHelper pathHelper,
             DashboardStatics dashboardStatics)
        {
            _chemicalRepository = chemicalRepository;
            _mapper = mapper;
            _uow = uow;
            _logger = logger;
            _webHostEnvironment = webHostEnvironment;
            _pathHelper = pathHelper;
            _dashboardStatics = dashboardStatics;
        }
        public async Task<ServiceResponse<ChemicalDto>> Handle(AddChemicalCommand request, CancellationToken cancellationToken)
        {

            var chemicalEntity = _mapper.Map<Chemical>(request);
            if (request.LstChemicalIndustries != null)
            {
                foreach (var industry in request.LstChemicalIndustries)
                {
                    chemicalEntity.ChemicalIndustries.Add(new ChemicalIndustry
                    {
                        ChemicalId = chemicalEntity.Id,
                        IndustryId = Guid.Parse(industry)
                    });
                }
            }
            if (!string.IsNullOrEmpty(request.ChemicalImage))
            {
                var imgageUrl = string.IsNullOrEmpty(request.CasNumber) ? request.Name + ".png" : request.CasNumber + ".png";
                chemicalEntity.Url = imgageUrl;
            }
            _chemicalRepository.Add(chemicalEntity);
            if (await _uow.SaveAsync() <= 0)
            {
                _logger.LogError("Creating a Chemical  failed on save.", request);
                return ServiceResponse<ChemicalDto>.ReturnFailed(500, $"Creating a Chemical  failed on save.");
            }

            if (!string.IsNullOrWhiteSpace(request.ChemicalImage))
            {
                string contentRootPath = _webHostEnvironment.WebRootPath;
                var pathToSave = Path.Combine(contentRootPath, _pathHelper.ChemicalImagePath, chemicalEntity.Url);
                await FileData.SaveFile(pathToSave, request.ChemicalImage);
            }
            _dashboardStatics.ChemicalCount = _dashboardStatics.ChemicalCount + 1;
            return ServiceResponse<ChemicalDto>.ReturnResultWith200(_mapper.Map<ChemicalDto>(chemicalEntity));
        }
    }
}
