using AutoMapper;
using ChemWebsite.Common.UnitOfWork;
using ChemWebsite.Data.Dto;
using ChemWebsite.Domain;
using ChemWebsite.Helper;
using ChemWebsite.MediatR.Commands;
using ChemWebsite.Repository;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ChemWebsite.MediatR.Handlers
{
    public class DeleteDocumentCategoryCommandHandler : IRequestHandler<DeleteDocumentCategoryCommand, ServiceResponse<DocumentCategoryDto>>
    {
        private readonly IDocumentCategoryRepository _categoryRepository;
        private readonly IUnitOfWork<ChemWebsiteDbContext> _uow;
        private readonly IMapper _mapper;
        public DeleteDocumentCategoryCommandHandler(
           IDocumentCategoryRepository categoryRepository,
            IMapper mapper,
            IUnitOfWork<ChemWebsiteDbContext> uow
            )
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
            _uow = uow;
        }
        public async Task<ServiceResponse<DocumentCategoryDto>> Handle(DeleteDocumentCategoryCommand request, CancellationToken cancellationToken)
        {
            var entityExist = await _categoryRepository.FindAsync(request.Id);
            if (entityExist == null)
            {
                return ServiceResponse<DocumentCategoryDto>.Return422("Category is not found.");
            }
            _categoryRepository.Delete(request.Id);
            if (await _uow.SaveAsync() <= 0)
            {
                return ServiceResponse<DocumentCategoryDto>.Return500();
            }
            var entityDto = _mapper.Map<DocumentCategoryDto>(entityExist);
            return ServiceResponse<DocumentCategoryDto>.ReturnResultWith200(entityDto);

        }
    }
}
