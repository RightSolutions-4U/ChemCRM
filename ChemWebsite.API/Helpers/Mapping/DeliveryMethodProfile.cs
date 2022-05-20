using AutoMapper;
using ChemWebsite.Data;
using ChemWebsite.Data.Dto;
using ChemWebsite.MediatR.CommandAndQuery;

namespace ChemWebsite.API.Helpers
{
    public class DeliveryMethodProfile : Profile
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public DeliveryMethodProfile()
        {
            CreateMap<DeliveryMethod, DeliveryMethodDto>().ReverseMap();
            CreateMap<AddDeliveryMethodCommand, DeliveryMethod>();
            CreateMap<UpdateDeliveryMethodCommand, DeliveryMethod>();
        }
    }
}
