using MediatR;
using ChemWebsite.Data.Resources;
using ChemWebsite.Repository;

namespace ChemWebsite.MediatR.CommandAndQuery
{
    public class GetUsersQuery : IRequest<UserList>
    {
        public UserResource UserResource { get; set; }
    }
}
