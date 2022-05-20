using ChemWebsite.Data;
using ChemWebsite.Repository;
using MediatR;

namespace ChemWebsite.MediatR.CommandAndQuery
{
    public class GetAllCustomerQuery : IRequest<CustomerList>
    {
        public CustomerResource CustomerResource { get; set; }
    }
}
