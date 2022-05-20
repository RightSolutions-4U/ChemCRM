using ChemWebsite.Common.GenericRepository;
using ChemWebsite.Data;
using ChemWebsite.Data.Resources;
using System.Threading.Tasks;

namespace ChemWebsite.Repository
{
    public interface ISupplierRepository :  IGenericRepository<Supplier>
    {
        Task<SupplierList> GetSuppliers(SupplierResource supplierResource);
    }
}
