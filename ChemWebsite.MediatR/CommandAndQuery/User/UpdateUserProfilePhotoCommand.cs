using MediatR;
using Microsoft.AspNetCore.Http;
using ChemWebsite.Data.Dto;
using ChemWebsite.Helper;

namespace ChemWebsite.MediatR.CommandAndQuery
{
    public class UpdateUserProfilePhotoCommand : IRequest<ServiceResponse<UserDto>>
    {
        public IFormFileCollection FormFile { get; set; }
    }
}
