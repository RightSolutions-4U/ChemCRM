using AutoMapper;
using ChemWebsite.Data;
using ChemWebsite.Data.Dto;
using ChemWebsite.MediatR.CommandAndQuery;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Threading;
using System.Threading.Tasks;
using System;
using ChemWebsite.Helper;
using Microsoft.Extensions.Logging;

namespace ChemWebsite.MediatR.Handlers
{
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, ServiceResponse<UserDto>>
    {
        private readonly UserManager<User> _userManager;
        private readonly UserInfoToken _userInfoToken;
        private readonly IMapper _mapper;
        private readonly ILogger<DeleteUserCommandHandler> _logger;
        private readonly DashboardStatics _dashboardStatics;

        public DeleteUserCommandHandler(
            UserManager<User> userManager,
            IMapper mapper,
            UserInfoToken userInfoToken,
            ILogger<DeleteUserCommandHandler> logger,
            DashboardStatics dashboardStatics)
        {
            _userManager = userManager;
            _mapper = mapper;
            _userInfoToken = userInfoToken;
            _logger = logger;
            _dashboardStatics = dashboardStatics;
        }

        public async Task<ServiceResponse<UserDto>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var appUser = await _userManager.FindByIdAsync(request.Id.ToString());
            if (appUser == null)
            {
                _logger.LogError("User does not exist.");
                return ServiceResponse<UserDto>.Return409("User does not exist.");
            }
            appUser.IsDeleted = true;
            appUser.DeletedDate = DateTime.Now.ToLocalTime();
            appUser.DeletedBy = Guid.Parse(_userInfoToken.Id);
            IdentityResult result = await _userManager.UpdateAsync(appUser);
            if (!result.Succeeded)
            {
                return ServiceResponse<UserDto>.Return500();
            }

            return ServiceResponse<UserDto>.ReturnResultWith200(_mapper.Map<UserDto>(appUser));
        }
    }
}
