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
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ChemWebsite.MediatR.Handlers
{
    public class UpdateChemicalCommandHanlder : IRequestHandler<UpdateChemicalCommand, ServiceResponse<ChemicalDto>>
    {
        private readonly IChemicalRepository _chemicalRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork<ChemWebsiteDbContext> _uow;
        private readonly IIndustryChemicalRepository _industryChemicalRepository;
        private readonly ILogger<UpdateChemicalCommandHanlder> _logger;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly PathHelper _pathHelper;

        public UpdateChemicalCommandHanlder(IChemicalRepository chemicalRepository,
            IUnitOfWork<ChemWebsiteDbContext> uow,
            IIndustryChemicalRepository industryChemicalRepository,
            ILogger<UpdateChemicalCommandHanlder> logger,
            IMapper mapper,
            IWebHostEnvironment webHostEnvironment,
            PathHelper pathHelper)
        {
            _chemicalRepository = chemicalRepository;
            _uow = uow;
            _industryChemicalRepository = industryChemicalRepository;
            _logger = logger;
            _mapper = mapper;
            _webHostEnvironment = webHostEnvironment;
            _pathHelper = pathHelper;
        }
        public async Task<ServiceResponse<ChemicalDto>> Handle(UpdateChemicalCommand request, CancellationToken cancellationToken)
        {

            var chemicalEntity = await _chemicalRepository.FindByInclude(c => c.Id == request.Id, c => c.ChemicalIndustries).FirstOrDefaultAsync();
            if (chemicalEntity == null)
            {
                _logger.LogError("Chemical not found", request);
                return ServiceResponse<ChemicalDto>.Return404();
            }

            var industriesToAdd = request.LstChemicalIndustries.Where(c => !chemicalEntity.ChemicalIndustries.Select(c => c.IndustryId).Contains(c))
                .Select(c => new ChemicalIndustry
                {
                    ChemicalId = chemicalEntity.Id,
                    IndustryId = c
                }).ToList();
            _industryChemicalRepository.AddRange(industriesToAdd);
            var industriesToDelete = chemicalEntity.ChemicalIndustries.Where(c => !request.LstChemicalIndustries.Select(cs => cs).Contains(c.IndustryId)).ToList();
            _industryChemicalRepository.RemoveRange(industriesToDelete);

            _mapper.Map(request, chemicalEntity);
            var imageUrl = string.IsNullOrEmpty(request.CasNumber) ? request.Name + ".png" : request.CasNumber + ".png";
            if (request.IsImageUpdate)
            {
                if (!string.IsNullOrEmpty(request.ChemicalImage))
                {
                    chemicalEntity.Url = imageUrl;
                }
                else
                {
                    chemicalEntity.Url = null;
                }
            }

            _chemicalRepository.Update(chemicalEntity);
            if (await _uow.SaveAsync() <= 0)
            {
                _logger.LogError("Error while updating Chemical", request);
                return ServiceResponse<ChemicalDto>.Return500();
            }

            if (request.IsImageUpdate)
            {
                if (!string.IsNullOrWhiteSpace(request.ChemicalImage))
                {
                    string contentRootPath = _webHostEnvironment.WebRootPath;
                    var pathToSave = Path.Combine(contentRootPath, _pathHelper.ChemicalImagePath, chemicalEntity.Url);
                    await FileData.SaveFile(pathToSave, request.ChemicalImage);
                }
                else
                {
                    string contentRootPath = _webHostEnvironment.WebRootPath;
                    var filePath = Path.Combine(contentRootPath, _pathHelper.ChemicalImagePath, imageUrl);
                    if (File.Exists(filePath))
                    {
                        FileData.DeleteFile(filePath);
                    }
                }
            }

            return ServiceResponse<ChemicalDto>.ReturnResultWith200(_mapper.Map<ChemicalDto>(chemicalEntity));
        }
    }
}
