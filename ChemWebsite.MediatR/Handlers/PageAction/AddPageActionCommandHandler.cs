using AutoMapper;
using ChemWebsite.Common.UnitOfWork;
using ChemWebsite.Data;
using ChemWebsite.Data.Dto;
using ChemWebsite.Domain;
using ChemWebsite.MediatR.CommandAndQuery;
using ChemWebsite.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;
using ChemWebsite.Helper;

namespace ChemWebsite.MediatR.Handlers
{
    public class AddPageActionCommandHandler : IRequestHandler<AddPageActionCommand, ServiceResponse<PageActionDto>>
    {
        private readonly IPageActionRepository _pageActionRepository;
        private readonly IUnitOfWork<ChemWebsiteDbContext> _uow;
        private readonly IMapper _mapper;
        public AddPageActionCommandHandler(
           IPageActionRepository pageActionRepository,
            IMapper mapper,
            IUnitOfWork<ChemWebsiteDbContext> uow
            )
        {
            _pageActionRepository = pageActionRepository;
            _mapper = mapper;
            _uow = uow;
        }

        public async Task<ServiceResponse<PageActionDto>> Handle(AddPageActionCommand request, CancellationToken cancellationToken)
        {
            var entity = await _pageActionRepository.FindBy(c => c.PageId == request.PageId && c.ActionId == request.ActionId).FirstOrDefaultAsync();
            if (entity == null)
            {
                entity = _mapper.Map<PageAction>(request);
                entity.Id = Guid.NewGuid();
                _pageActionRepository.Add(entity);
                if (await _uow.SaveAsync() <= 0)
                {
                    return ServiceResponse<PageActionDto>.Return500();
                }
            }
            return ServiceResponse<PageActionDto>.ReturnResultWith200(_mapper.Map<PageActionDto>(entity));
        }
    }
}
