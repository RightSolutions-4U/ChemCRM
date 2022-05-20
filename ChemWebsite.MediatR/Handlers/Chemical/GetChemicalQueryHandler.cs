using AutoMapper;
using ChemWebsite.Data.Dto;
using ChemWebsite.Helper;
using ChemWebsite.MediatR.CommandAndQuery;
using ChemWebsite.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace ChemWebsite.MediatR.Handlers
{
    public class GetChemicalQueryHandler : IRequestHandler<GetChemicalQuery, ServiceResponse<ChemicalDto>>
    {
        private readonly IChemicalRepository _chemicalRespository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetChemicalQueryHandler> _logger;
        private readonly PathHelper _pathHelper;

        public GetChemicalQueryHandler(IChemicalRepository chemicalRespository,
            IMapper mapper,
            ILogger<GetChemicalQueryHandler> logger,
            PathHelper pathHelper)
        {
            _chemicalRespository = chemicalRespository;
            _mapper = mapper;
            _logger = logger;
            _pathHelper = pathHelper;
        }
        public async Task<ServiceResponse<ChemicalDto>> Handle(GetChemicalQuery request, CancellationToken cancellationToken)
        {
            var enitity = await _chemicalRespository.All.Include(c => c.ChemicalIndustries).FirstOrDefaultAsync(c => c.Id == request.Id);
            if (enitity == null)
            {
                _logger.LogError("Chemical not found", request.Id);
                return ServiceResponse<ChemicalDto>.Return404("Chemical not found");
            }
            var entityDto = _mapper.Map<ChemicalDto>(enitity);
            entityDto.Url = string.IsNullOrWhiteSpace(entityDto.Url) ? "" : Path.Combine(_pathHelper.ChemicalImagePath, entityDto.Url);
            return ServiceResponse<ChemicalDto>.ReturnResultWith200(entityDto);
        }
    }
}
