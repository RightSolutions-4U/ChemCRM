using AutoMapper;
using ChemWebsite.Common.UnitOfWork;
using ChemWebsite.Data;
using ChemWebsite.Data.Dto;
using ChemWebsite.Domain;
using ChemWebsite.MediatR.CommandAndQuery;
using ChemWebsite.Repository;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ChemWebsite.Helper;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace ChemWebsite.MediatR.Handlers
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, ServiceResponse<UserDto>>
    {
        private readonly UserManager<User> _userManager;
        private readonly IUserRoleRepository _userRoleRepository;
        private readonly IUnitOfWork<ChemWebsiteDbContext> _uow;
        private readonly IMapper _mapper;
        private readonly UserInfoToken _userInfoToken;
        private readonly IUserAllowedIPRepository _userAllowedIPRepository;
        private readonly ILogger<UpdateUserCommandHandler> _logger;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly PathHelper _pathHelper;

        public UpdateUserCommandHandler(
            IUserRoleRepository userRoleRepository,
            IMapper mapper,
            IUnitOfWork<ChemWebsiteDbContext> uow,
            UserManager<User> userManager,
            UserInfoToken userInfoToken,
            IUserAllowedIPRepository userAllowedIPRepository,
            ILogger<UpdateUserCommandHandler> logger,
            IWebHostEnvironment webHostEnvironment,
            PathHelper pathHelper
            )
        {
            _mapper = mapper;
            _userManager = userManager;
            _userRoleRepository = userRoleRepository;
            _uow = uow;
            _userInfoToken = userInfoToken;
            _userAllowedIPRepository = userAllowedIPRepository;
            _logger = logger;
            _webHostEnvironment = webHostEnvironment;
            _pathHelper = pathHelper;
        }

        public async Task<ServiceResponse<UserDto>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var appUser = await _userManager.FindByIdAsync(request.Id.ToString());
            if (appUser == null)
            {
                _logger.LogError("User does not exist.");
                return ServiceResponse<UserDto>.Return409("User does not exist.");
            }
            appUser.FirstName = request.FirstName;
            appUser.LastName = request.LastName;
            appUser.PhoneNumber = request.PhoneNumber;
            appUser.Address = request.Address;
            appUser.IsActive = request.IsActive;
            appUser.ModifiedDate = DateTime.Now.ToLocalTime();
            appUser.ModifiedBy = Guid.Parse(_userInfoToken.Id);

            var oldProfilePhoto = appUser.ProfilePhoto;
            if (request.IsImageUpdate)
            {
                if (!string.IsNullOrEmpty(request.ImgSrc))
                {
                    appUser.ProfilePhoto = $"{Guid.NewGuid()}.png";
                }
                else
                {
                    appUser.ProfilePhoto = null;
                }
            }

            IdentityResult result = await _userManager.UpdateAsync(appUser);

            // Update User's Role
            var userRoles = _userRoleRepository.All.Where(c => c.UserId == appUser.Id).ToList();
            var rolesToAdd = request.UserRoles.Where(c => !userRoles.Select(c => c.RoleId).Contains(c.RoleId)).ToList();
            _userRoleRepository.AddRange(_mapper.Map<List<UserRole>>(rolesToAdd));
            var rolesToDelete = userRoles.Where(c => !request.UserRoles.Select(cs => cs.RoleId).Contains(c.RoleId)).ToList();
            _userRoleRepository.RemoveRange(rolesToDelete);

            // Update User's Allowed IPs
            var userAllowedIPs = _userAllowedIPRepository.All.Where(c => c.UserId == appUser.Id).ToList();
            var ipsToAdd = request.UserAllowedIPs
                .Where(c => !userAllowedIPs.Select(c => c.IPAddress).Contains(c.IPAddress))
                .Select(cs => new UserAllowedIP
                {
                    IPAddress = cs.IPAddress,
                    UserId = appUser.Id
                })
                .ToList();
            _userAllowedIPRepository.AddRange(ipsToAdd);
            var ipsToDelete = userAllowedIPs
                .Where(c => !request.UserAllowedIPs.Select(cs => cs.IPAddress).Contains(c.IPAddress))
                .ToList();
            _userAllowedIPRepository.RemoveRange(ipsToDelete);

            if (await _uow.SaveAsync() <= 0 && !result.Succeeded)
            {
                return ServiceResponse<UserDto>.Return500();
            }

            if (request.IsImageUpdate)
            {
                string contentRootPath = _webHostEnvironment.WebRootPath;
                // delete old file
                if (!string.IsNullOrWhiteSpace(oldProfilePhoto))
                {
                    var oldFile = Path.Combine(contentRootPath, _pathHelper.UserProfilePath, oldProfilePhoto);
                    if (File.Exists(oldFile))
                    {
                        FileData.DeleteFile(oldFile);
                    }
                }
                // save new file
                if (!string.IsNullOrWhiteSpace(request.ImgSrc))
                {
                    var pathToSave = Path.Combine(contentRootPath, _pathHelper.UserProfilePath, appUser.ProfilePhoto);
                    await FileData.SaveFile(pathToSave, request.ImgSrc);
                }
            }
            return ServiceResponse<UserDto>.ReturnResultWith200(_mapper.Map<UserDto>(appUser));
        }
    }
}
