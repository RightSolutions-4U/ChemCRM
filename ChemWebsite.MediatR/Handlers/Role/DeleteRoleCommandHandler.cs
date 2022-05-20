using ChemWebsite.Data.Dto;
using ChemWebsite.MediatR.CommandAndQuery;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using ChemWebsite.Repository;
using System;
using ChemWebsite.Domain;
using ChemWebsite.Common.UnitOfWork;
using ChemWebsite.Helper;
using Microsoft.Extensions.Logging;

namespace ChemWebsite.MediatR.Handlers
{
    public class DeleteRoleCommandHandler : IRequestHandler<DeleteRoleCommand, ServiceResponse<RoleDto>>
    {
        private readonly UserInfoToken _userInfoToken;
        private readonly IRoleRepository _roleRepository;
        private readonly IUnitOfWork<ChemWebsiteDbContext> _uow;
        private readonly ILogger<DeleteRoleCommandHandler> _logger;
        public DeleteRoleCommandHandler(
            UserInfoToken userInfoToken,
            IRoleRepository roleRepository,
            IUnitOfWork<ChemWebsiteDbContext> uow,
            ILogger<DeleteRoleCommandHandler> logger
            )
        {
            _userInfoToken = userInfoToken;
            _roleRepository = roleRepository;
            _uow = uow;
            _logger = logger;
        }

        public async Task<ServiceResponse<RoleDto>> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
        {
            var entityExist = await _roleRepository.FindAsync(request.Id);
            if (entityExist == null)
            {
                _logger.LogError("Not found");
                return ServiceResponse<RoleDto>.Return404();
            }
            entityExist.IsDeleted = true;
            entityExist.DeletedBy = Guid.Parse(_userInfoToken.Id);
            entityExist.DeletedDate = DateTime.Now.ToLocalTime();
            _roleRepository.Update(entityExist);
            if (await _uow.SaveAsync() <= 0)
            {
                return ServiceResponse<RoleDto>.Return500();
            }
            return ServiceResponse<RoleDto>.ReturnResultWith204();
        }
    }
}
