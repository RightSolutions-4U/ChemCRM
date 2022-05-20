using AutoMapper;
using ChemWebsite.Common.UnitOfWork;
using ChemWebsite.Data;
using ChemWebsite.Data.Dto;
using ChemWebsite.Domain;
using ChemWebsite.MediatR.CommandAndQuery;
using ChemWebsite.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;
using ChemWebsite.Helper;

namespace ChemWebsite.MediatR.Handlers
{
    public class AddPageCommandHandler : IRequestHandler<AddPageCommand, ServiceResponse<PageDto>>
    {
        private readonly IPageRepository _pageRepository;
        private readonly IUnitOfWork<ChemWebsiteDbContext> _uow;
        private readonly IMapper _mapper;
        private readonly ILogger<AddPageCommandHandler> _logger;
        public AddPageCommandHandler(
           IPageRepository pageRepository,
            IMapper mapper,
            IUnitOfWork<ChemWebsiteDbContext> uow,
            ILogger<AddPageCommandHandler> logger
            )
        {
            _pageRepository = pageRepository;
            _mapper = mapper;
            _uow = uow;
            _logger = logger;
        }
        public async Task<ServiceResponse<PageDto>> Handle(AddPageCommand request, CancellationToken cancellationToken)
        {
            var existingEntity = await _pageRepository.FindBy(c => c.Name == request.Name).FirstOrDefaultAsync();
            if (existingEntity != null)
            {
                _logger.LogError("Page Already Exist");
                return ServiceResponse<PageDto>.Return409("Page Already Exist.");
            }
            var entity = _mapper.Map<Page>(request);
            entity.Id = Guid.NewGuid();
            _pageRepository.Add(entity);
            if (await _uow.SaveAsync() <= 0)
            {

                _logger.LogError("Save Page have Error");
                return ServiceResponse<PageDto>.Return500();
            }
            return ServiceResponse<PageDto>.ReturnResultWith200(_mapper.Map<PageDto>(entity));
        }
    }
}
