using ChemWebsite.Common.GenericRepository;
using ChemWebsite.Data;
using ChemWebsite.Data.Dto;
using System.Threading.Tasks;
using ChemWebsite.Data.Resources;

namespace ChemWebsite.Repository
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<UserList> GetUsers(UserResource userResource);
        Task<UserAuthDto> BuildUserAuthObject(User appUser);
    }
}
