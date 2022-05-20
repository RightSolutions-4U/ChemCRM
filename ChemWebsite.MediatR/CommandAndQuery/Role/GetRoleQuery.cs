using ChemWebsite.Data.Dto;
using MediatR;
using System;
using ChemWebsite.Helper;

namespace ChemWebsite.MediatR.CommandAndQuery
{
    public class GetRoleQuery: IRequest<ServiceResponse<RoleDto>>
    {
        public Guid Id { get; set; }
    }
}
