using ChemWebsite.Data.Dto;
using ChemWebsite.Helper;
using MediatR;

namespace ChemWebsite.MediatR.CommandAndQuery
{
   public class AddDeliveryMethodCommand : IRequest<ServiceResponse<DeliveryMethodDto>>
    {
        public string Name { get; set; }
    }
}
