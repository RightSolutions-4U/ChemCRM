using ChemWebsite.Common.GenericRepository;
using ChemWebsite.Data;
using System.Threading.Tasks;

namespace ChemWebsite.Repository
{
    public interface ICustomerRepository : IGenericRepository<Customer>
    {
        Task<CustomerList> GetCustomers(CustomerResource customerResource);
    }
}
