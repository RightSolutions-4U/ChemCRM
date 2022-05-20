using ChemWebsite.Data.Resources;
using ChemWebsite.Repository;
using MediatR;

namespace ChemWebsite.MediatR.CommandAndQuery
{
    public class GetAllPurchaseOrderQuery : IRequest<PurchaseOrderList>
    {
        public PurchaseOrderResource PurchaseOrderResource { get; set; }
    }
}
