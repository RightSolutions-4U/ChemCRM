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
using System.Threading;
using System.Threading.Tasks;

namespace ChemWebsite.MediatR.Handlers
{
    public class UpdateChemicalTypeCommandHandler : IRequestHandler<UpdateChemicalTypeCommand, ServiceResponse<ChemicalTypeDto>>
    {
        private readonly IChemicalTypeRepository _chemicalTypeRepository;
        private readonly IUnitOfWork<ChemWebsiteDbContext> _uow;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateChemicalTypeCommandHandler> _logger;

        public UpdateChemicalTypeCommandHandler(
            IChemicalTypeRepository chemicalTypeRepository,
            IUnitOfWork<ChemWebsiteDbContext> uow,
            IMapper mapper,
            ILogger<UpdateChemicalTypeCommandHandler> logger)
        {
            _chemicalTypeRepository = chemicalTypeRepository;
            _uow = uow;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<ServiceResponse<ChemicalTypeDto>> Handle(UpdateChemicalTypeCommand request, CancellationToken cancellationToken)
        {
            var entityExist = await _chemicalTypeRepository.FindBy(c => c.Name == request.Name && c.Id != request.Id)
                .FirstOrDefaultAsync();
            if (entityExist != null)
            {
                _logger.LogError("Chemical Type Name Already Exist.");
                return ServiceResponse<ChemicalTypeDto>.Return409("Chemical Type Name Already Exist.");
            }
            entityExist = await _chemicalTypeRepository.FindBy(v => v.Id == request.Id).FirstOrDefaultAsync();
            entityExist.Name = request.Name;
            entityExist.Description = request.Description;
            _chemicalTypeRepository.Update(entityExist);
            if (await _uow.SaveAsync() <= 0)
            {
                return ServiceResponse<ChemicalTypeDto>.Return500();
            }
            var entityDto = _mapper.Map<ChemicalTypeDto>(entityExist);
            return ServiceResponse<ChemicalTypeDto>.ReturnSuccess();
        }
    }
}
