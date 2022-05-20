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
    public class AddDocumentCommandHandler : IRequestHandler<AddDocumentCommand, ServiceResponse<DocumentDto>>
    {
        private readonly IDocumentRepository _documentRepository;
        private readonly IUnitOfWork<ChemWebsiteDbContext> _uow;
        private readonly IMapper _mapper;
        private readonly UserInfoToken _userInfo;
        public AddDocumentCommandHandler(
            IDocumentRepository documentRepository,
            IMapper mapper,
            IUnitOfWork<ChemWebsiteDbContext> uow,
            UserInfoToken userInfo
            )
        {
            _documentRepository = documentRepository;
            _mapper = mapper;
            _uow = uow;
            _userInfo = userInfo;
        }

        public async Task<ServiceResponse<DocumentDto>> Handle(AddDocumentCommand request, CancellationToken cancellationToken)
        {
            var entityExist = await _documentRepository.FindBy(c => c.Name == request.Name).FirstOrDefaultAsync();
            if (entityExist != null)
            {
                return ServiceResponse<DocumentDto>.ReturnFailed(409, "Document already exist.");
            }
            var entity = _mapper.Map<Document>(request);
            entity.CreatedBy = Guid.Parse(_userInfo.Id);
            entity.CreatedDate = DateTime.Now;
            _documentRepository.Add(entity);
            if (await _uow.SaveAsync() <= 0)
            {
                return ServiceResponse<DocumentDto>.Return500();
            }
            var entityDto = _mapper.Map<DocumentDto>(entity);
            return ServiceResponse<DocumentDto>.ReturnResultWith201(entityDto);
        }
    }
}
