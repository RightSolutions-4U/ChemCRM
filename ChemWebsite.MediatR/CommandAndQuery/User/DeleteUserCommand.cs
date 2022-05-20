using ChemWebsite.Data.Dto;
using MediatR;
using System;
using ChemWebsite.Helper;

namespace ChemWebsite.MediatR.CommandAndQuery
{
    public class DeleteUserCommand : IRequest<ServiceResponse<UserDto>>
    {
        public Guid Id { get; set; }
    }
}
