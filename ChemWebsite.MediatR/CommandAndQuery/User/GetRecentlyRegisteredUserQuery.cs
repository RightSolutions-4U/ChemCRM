using MediatR;
using System.Collections.Generic;
using ChemWebsite.Data.Dto;

namespace ChemWebsite.MediatR.CommandAndQuery
{
    public class GetRecentlyRegisteredUserQuery : IRequest<List<UserDto>>
    {
    }
}
