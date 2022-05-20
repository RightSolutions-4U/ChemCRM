using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;
using ChemWebsite.Common.UnitOfWork;
using ChemWebsite.Data.Dto;
using ChemWebsite.Domain;
using ChemWebsite.Helper;
using ChemWebsite.MediatR.CommandAndQuery;
using ChemWebsite.Repository;

namespace ChemWebsite.MediatR.Handlers
{
    public class DeleteEmailTemplateCommandHandler : IRequestHandler<DeleteEmailTemplateCommand, ServiceResponse<bool>>
    {
        private readonly IEmailTemplateRepository _emailTemplateRepository;
        private readonly IUnitOfWork<ChemWebsiteDbContext> _uow;
        private readonly UserInfoToken _userInfoToken;
        private readonly ILogger<DeleteEmailTemplateCommandHandler> _logger;
        public DeleteEmailTemplateCommandHandler(
           IEmailTemplateRepository emailTemplateRepository,
            IUnitOfWork<ChemWebsiteDbContext> uow,
            UserInfoToken userInfoToken,
            ILogger<DeleteEmailTemplateCommandHandler> logger
            )
        {
            _emailTemplateRepository = emailTemplateRepository;
            _uow = uow;
            _userInfoToken = userInfoToken;
            _logger = logger;
        }

        public async Task<ServiceResponse<bool>> Handle(DeleteEmailTemplateCommand request, CancellationToken cancellationToken)
        {
            var entityExist = await _emailTemplateRepository.FindAsync(request.Id);
            if (entityExist == null)
            {
                _logger.LogError("Email Template Not Found.");
                return ServiceResponse<bool>.Return404();
            }
            entityExist.IsDeleted = true;
            _emailTemplateRepository.Update(entityExist);
            if (await _uow.SaveAsync() <= 0)
            {
                return ServiceResponse<bool>.Return500();
            }
            return ServiceResponse<bool>.ReturnResultWith204();
        }
    }
}
