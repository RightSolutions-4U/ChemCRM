using ChemWebsite.Data.Resources;
using ChemWebsite.Repository;
using MediatR;

namespace ChemWebsite.MediatR.CommandAndQuery
{
    public class GetAllSupplierQuery : IRequest<SupplierList>
    {
        public SupplierResource SupplierResource { get; set; }
    }
}
