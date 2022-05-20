using AutoMapper;
using ChemWebsite.Common.UnitOfWork;
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
using Microsoft.Extensions.Logging;

namespace ChemWebsite.MediatR.Handlers
{
    public class AddActionCommandHandler : IRequestHandler<AddActionCommand, ServiceResponse<ActionDto>>
    {
        private readonly IActionRepository _actionRepository;
        private readonly IUnitOfWork<ChemWebsiteDbContext> _uow;
        private readonly IMapper _mapper;
        private readonly ILogger<AddActionCommandHandler> _logger;
        public AddActionCommandHandler(
           IActionRepository actionRepository,
            IMapper mapper,
            IUnitOfWork<ChemWebsiteDbContext> uow,
            ILogger<AddActionCommandHandler> logger
            )
        {
            _actionRepository = actionRepository;
            _mapper = mapper;
            _uow = uow;
            _logger = logger;
        }
        public async Task<ServiceResponse<ActionDto>> Handle(AddActionCommand request, CancellationToken cancellationToken)
        {
            var entityExist = await _actionRepository.FindBy(c => c.Name.Trim().ToLower() == request.Name.Trim().ToLower()).FirstOrDefaultAsync();
            if (entityExist != null)
            {
                _logger.LogError("Action already exist.");
                return ServiceResponse<ActionDto>.Return409("Action already exist.");
            }
            var entity = _mapper.Map<Data.Action>(request);
            entity.Id = Guid.NewGuid();
            _actionRepository.Add(entity);
            if (await _uow.SaveAsync() <= 0)
            {
                return ServiceResponse<ActionDto>.Return500();
            }
            var entityDto = _mapper.Map<ActionDto>(entity);
            return ServiceResponse<ActionDto>.ReturnResultWith200(entityDto);
        }
    }
}
