using AutoMapper;
using ChemWebsite.Data.Dto;
using ChemWebsite.MediatR.Queries;
using ChemWebsite.Repository;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ChemWebsite.MediatR.Handlers
{
    public class GetDocumentCategoryQueryHandler : IRequestHandler<GetDocumentCategoryQuery, DocumentCategoryDto>
    {
        private readonly IDocumentCategoryRepository _categoryRepository;
        private readonly IMapper _mapper;
        public GetDocumentCategoryQueryHandler(
           IDocumentCategoryRepository categoryRepository,
            IMapper mapper
            )
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<DocumentCategoryDto> Handle(GetDocumentCategoryQuery request, CancellationToken cancellationToken)
        {
            var entity = await _categoryRepository.FindAsync(request.Id);
            return _mapper.Map<DocumentCategoryDto>(entity);
        }
    }
}
