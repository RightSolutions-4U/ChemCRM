using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using ChemWebsite.Common.UnitOfWork;
using ChemWebsite.Data;
using ChemWebsite.Data.Dto;
using ChemWebsite.Domain;
using ChemWebsite.Helper;
using ChemWebsite.MediatR.CommandAndQuery;
using ChemWebsite.Repository;

namespace ChemWebsite.MediatR.Handlers
{
    public class AddLogCommandHandler : IRequestHandler<AddLogCommand, ServiceResponse<NLogDto>>
    {
        private readonly INLogRepository _nLogRepository;
        private readonly IUnitOfWork<ChemWebsiteDbContext> _uow;
        public AddLogCommandHandler(
           INLogRepository nLogRepository,
            IUnitOfWork<ChemWebsiteDbContext> uow
            )
        {
            _nLogRepository = nLogRepository;
            _uow = uow;
        }
        public async Task<ServiceResponse<NLogDto>> Handle(AddLogCommand request, CancellationToken cancellationToken)
        {
            _nLogRepository.Add(new NLog
            {
                Id = Guid.NewGuid(),
                Logged = DateTime.Now.ToLocalTime(),
                Level = "Error",
                Message = request.ErrorMessage,
                Source = "Angular",
                Exception = request.Stack
            });
            await _uow.SaveAsync();
            return ServiceResponse<NLogDto>.ReturnSuccess();
        }
    }
}
