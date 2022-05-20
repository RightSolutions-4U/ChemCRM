using ChemWebsite.Data.Dto;
using ChemWebsite.Helper;
using MediatR;


namespace ChemWebsite.MediatR.CommandAndQuery
{
    public class AddPaymentTermCommand : IRequest<ServiceResponse<PaymentTermDto>>
    {
        public string Name { get; set; }
    }
}
