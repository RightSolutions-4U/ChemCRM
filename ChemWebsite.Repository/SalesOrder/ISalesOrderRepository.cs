using ChemWebsite.Common.GenericRepository;
using ChemWebsite.Data;
using ChemWebsite.Data.Resources;
using System.Threading.Tasks;

namespace ChemWebsite.Repository
{
    public interface ISalesOrderRepository : IGenericRepository<SalesOrder>
    {
        Task<SalesOrderList> GetSalesOrders(SaleOrderResource saleOrderResource);
    }
}
