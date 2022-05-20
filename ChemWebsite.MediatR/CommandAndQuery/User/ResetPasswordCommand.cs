using ChemWebsite.Data.Dto;
using MediatR;
using ChemWebsite.Helper;

namespace ChemWebsite.MediatR.CommandAndQuery
{
    public class ResetPasswordCommand : IRequest<ServiceResponse<UserDto>>
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
