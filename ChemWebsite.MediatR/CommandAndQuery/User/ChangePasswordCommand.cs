using ChemWebsite.Data.Dto;
using MediatR;
using ChemWebsite.Helper;

namespace ChemWebsite.MediatR.CommandAndQuery
{
    public class ChangePasswordCommand : IRequest<ServiceResponse<UserDto>>
    {
        public string UserName { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
    }
}
