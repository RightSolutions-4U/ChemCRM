using AutoMapper;
using ChemWebsite.Data.Dto;
using ChemWebsite.Helper;
using ChemWebsite.MediatR.CommandAndQuery;
using ChemWebsite.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ChemWebsite.MediatR.Handlers
{
    public class GetAllPackagingTypeQueryHandler
        : IRequestHandler<GetAllPackagingTypeQuery, List<PackagingTypeDto>>
    {
        private readonly IPackagingTypeRepository _packagingTypeRepository;
        private readonly IMapper _mapper;

        public GetAllPackagingTypeQueryHandler(
            IPackagingTypeRepository packagingTypeRepository,
            IMapper mapper)
        {
            _packagingTypeRepository = packagingTypeRepository;
            _mapper = mapper;
        }
        public async Task<List<PackagingTypeDto>> Handle(GetAllPackagingTypeQuery request, CancellationToken cancellationToken)
        {
            var entities = await _packagingTypeRepository.All.ToListAsync();
            var dtoEntities = _mapper.Map<List<PackagingTypeDto>>(entities);
            return dtoEntities;
        }
    }
}
