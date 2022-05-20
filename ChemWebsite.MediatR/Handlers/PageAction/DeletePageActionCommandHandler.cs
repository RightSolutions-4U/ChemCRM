using AutoMapper;
using ChemWebsite.Common.UnitOfWork;
using ChemWebsite.Data.Dto;
using ChemWebsite.Domain;
using ChemWebsite.MediatR.CommandAndQuery;
using ChemWebsite.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using ChemWebsite.Helper;

namespace ChemWebsite.MediatR.Handlers
{
    public class DeletePageActionCommandHandler : IRequestHandler<DeletePageActionCommand, ServiceResponse<PageActionDto>>
    {
        private readonly IPageActionRepository _pageActionRepository;
        private readonly IUnitOfWork<ChemWebsiteDbContext> _uow;
        private readonly IMapper _mapper;
        public DeletePageActionCommandHandler(
           IPageActionRepository pageActionRepository,
            IMapper mapper,
            IUnitOfWork<ChemWebsiteDbContext> uow
            )
        {
            _pageActionRepository = pageActionRepository;
            _mapper = mapper;
            _uow = uow;
        }

        public async Task<ServiceResponse<PageActionDto>> Handle(DeletePageActionCommand request, CancellationToken cancellationToken)
        {
            var entityExist = await _pageActionRepository.FindBy(c => c.Id == request.Id).FirstOrDefaultAsync();
            if (entityExist == null)
            {
                return ServiceResponse<PageActionDto>.Return404();
            }
            _pageActionRepository.Remove(entityExist);
            if (await _uow.SaveAsync() <= 0)
            {
                return ServiceResponse<PageActionDto>.Return500();
            }
            return ServiceResponse<PageActionDto>.ReturnSuccess();
        }
    }
}
