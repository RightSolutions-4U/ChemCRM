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

namespace ChemWebsite.MediatR.Handlers
{
    public class AddDocumentCategoryCommandHandler : IRequestHandler<AddDocumentCategoryCommand, ServiceResponse<DocumentCategoryDto>>
    {
        private readonly IDocumentCategoryRepository _categoryRepository;
        private readonly IUnitOfWork<ChemWebsiteDbContext> _uow;
        private readonly IMapper _mapper;
        public AddDocumentCategoryCommandHandler(
           IDocumentCategoryRepository categoryRepository,
            IMapper mapper,
            IUnitOfWork<ChemWebsiteDbContext> uow
            )
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
            _uow = uow;
        }
        public async Task<ServiceResponse<DocumentCategoryDto>> Handle(AddDocumentCategoryCommand request, CancellationToken cancellationToken)
        {
            var entityExist = await _categoryRepository.FindBy(c => c.Name == request.Name).FirstOrDefaultAsync();
            if (entityExist != null)
            {
                return ServiceResponse<DocumentCategoryDto>.Return422("Category Name already exist.");
            }
            var entity = _mapper.Map<DocumentCategory>(request);
            entity.Id = Guid.NewGuid();
            _categoryRepository.Add(entity);
            if (await _uow.SaveAsync() <= 0)
            {
                return ServiceResponse<DocumentCategoryDto>.Return500();
            }
            var entityDto = _mapper.Map<DocumentCategoryDto>(entity);
            return ServiceResponse<DocumentCategoryDto>.ReturnResultWith200(entityDto);
        }
    }
}
