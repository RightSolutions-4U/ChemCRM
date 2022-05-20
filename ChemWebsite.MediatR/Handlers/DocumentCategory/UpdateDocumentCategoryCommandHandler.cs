using AutoMapper;
using ChemWebsite.Common.UnitOfWork;
using ChemWebsite.Data.Dto;
using ChemWebsite.Data.Entities;
using ChemWebsite.Domain;
using ChemWebsite.Helper;
using ChemWebsite.MediatR.Commands;
using ChemWebsite.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace DocumentManagement.MediatR.Handlers
{
    public class UpdateDocumentCategoryCommandHandler : IRequestHandler<UpdateDocumentCategoryCommand, ServiceResponse<DocumentCategoryDto>>
    {
        private readonly IDocumentCategoryRepository _categoryRepository;
        private readonly IUnitOfWork<ChemWebsiteDbContext> _uow;
        private readonly IMapper _mapper;
        public UpdateDocumentCategoryCommandHandler(
           IDocumentCategoryRepository categoryRepository,
            IMapper mapper,
            IUnitOfWork<ChemWebsiteDbContext> uow
            )
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
            _uow = uow;
        }
        public async Task<ServiceResponse<DocumentCategoryDto>> Handle(UpdateDocumentCategoryCommand request, CancellationToken cancellationToken)
        {
            var entityExist = await _categoryRepository.FindBy(c => c.Name == request.Name && c.Id != request.Id).FirstOrDefaultAsync();
            if (entityExist != null)
            {
                return ServiceResponse<DocumentCategoryDto>.Return409("Category is not found.");
            }
            var entity = _mapper.Map<DocumentCategory>(request);
            entity.Id = Guid.NewGuid();
            _categoryRepository.Add(entity);
            if (await _uow.SaveAsync() <= 0)
            {
                return ServiceResponse<DocumentCategoryDto>.Return500();
            }

            var entityDto = _mapper.Map<DocumentCategoryDto>(entityExist);
            return ServiceResponse<DocumentCategoryDto>.ReturnResultWith200(entityDto);
        }
    }
}
