using MediatR;

namespace ChemWebsite.MediatR.CommandAndQuery
{
    public class GetNewPurchaseOrderNumberQuery : IRequest<string>
    {
    }
}
