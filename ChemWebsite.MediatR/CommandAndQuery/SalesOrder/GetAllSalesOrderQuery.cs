using ChemWebsite.Data.Resources;
using ChemWebsite.Repository;
using MediatR;


namespace ChemWebsite.MediatR.CommandAndQuery
{
    public class GetAllSalesOrderQuery : IRequest<SalesOrderList>
    {
        public SaleOrderResource SaleOrderResource { get; set; }
    }
}
