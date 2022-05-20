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
using Microsoft.Extensions.Logging;

namespace ChemWebsite.MediatR.Handlers
{
    public class AddRoleCommandHandler : IRequestHandler<AddRoleCommand, ServiceResponse<RoleDto>>
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IUnitOfWork<ChemWebsiteDbContext> _uow;
        private readonly IMapper _mapper;
        private readonly UserInfoToken _userInfoToken;
        private readonly ILogger<AddRoleCommandHandler> _logger;
        public AddRoleCommandHandler(
           IRoleRepository roleRepository,
            IMapper mapper,
            IUnitOfWork<ChemWebsiteDbContext> uow,
            UserInfoToken userInfoToken,
            ILogger<AddRoleCommandHandler> logger
            )
        {
            _roleRepository = roleRepository;
            _mapper = mapper;
            _uow = uow;
            _userInfoToken = userInfoToken;
            _logger = logger;
        }
        public async Task<ServiceResponse<RoleDto>> Handle(AddRoleCommand request, CancellationToken cancellationToken)
        {
            var entityExist = await _roleRepository.FindBy(c => c.Name == request.Name).FirstOrDefaultAsync();
            if (entityExist != null)
            {
                _logger.LogError("Role Name already exist.");
                return ServiceResponse<RoleDto>.Return409("Role Name already exist.");
            }

            request.RoleClaims.ForEach(rc => rc.ClaimType = rc.ClaimType.Trim().Replace(" ", "_"));
            var entity = _mapper.Map<Role>(request);
            entity.Id = Guid.NewGuid();
            entity.CreatedBy = Guid.Parse(_userInfoToken.Id);
            entity.CreatedDate = DateTime.Now.ToLocalTime();
            entity.ModifiedBy = Guid.Parse(_userInfoToken.Id);
            entity.NormalizedName = entity.Name;
            _roleRepository.Add(entity);
            if (await _uow.SaveAsync() <= 0)
            {
                return ServiceResponse<RoleDto>.Return500();
            }
            var entityDto = _mapper.Map<RoleDto>(entity);
            return ServiceResponse<RoleDto>.ReturnResultWith200(entityDto);
        }
    }
}
