using AutoMapper;
using ChemWebsite.Data.Dto;
using ChemWebsite.Helper;
using ChemWebsite.MediatR.CommandAndQuery;
using ChemWebsite.Repository;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ChemWebsite.MediatR.Handlers
{
    public class GetPackagingTypeQueryHandler 
        : IRequestHandler<GetPackagingTypeQuery, ServiceResponse<PackagingTypeDto>>
    {
        private readonly IPackagingTypeRepository _packagingTypeRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetPackagingTypeQueryHandler> _logger;

        public GetPackagingTypeQueryHandler(
            IPackagingTypeRepository packagingTypeRepository,
            IMapper mapper,
            ILogger<GetPackagingTypeQueryHandler> logger)
        {
            _packagingTypeRepository = packagingTypeRepository;
            _mapper = mapper;
            _logger = logger;

        }
        public async Task<ServiceResponse<PackagingTypeDto>> Handle(GetPackagingTypeQuery request, CancellationToken cancellationToken)
        {
            var entity = await _packagingTypeRepository.FindAsync(request.Id);
            if (entity != null)
                return ServiceResponse<PackagingTypeDto>.ReturnResultWith200(_mapper.Map<PackagingTypeDto>(entity));
            else
            {
                _logger.LogError("Not found");
                return ServiceResponse<PackagingTypeDto>.Return404();
            }
        }
    }
}
