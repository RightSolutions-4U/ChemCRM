using AutoMapper;
using ChemWebsite.Data.Dto;
using ChemWebsite.MediatR.Queries;
using ChemWebsite.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ChemWebsite.MediatR.Handlers
{
    public class GetAllDocumentCategoryQueryHandler : IRequestHandler<GetAllDocumentCategoryQuery, List<DocumentCategoryDto>>
    {
        private readonly IDocumentCategoryRepository _categoryRepository;
        private readonly IMapper _mapper;
        public GetAllDocumentCategoryQueryHandler(
           IDocumentCategoryRepository categoryRepository,
            IMapper mapper
            )
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }
        public async Task<List<DocumentCategoryDto>> Handle(GetAllDocumentCategoryQuery request, CancellationToken cancellationToken)
        {
            var entities = await _categoryRepository.All.ToListAsync();
            return _mapper.Map<List<DocumentCategoryDto>>(entities);
        }
    }
}
