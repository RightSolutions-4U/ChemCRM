using AutoMapper;
using ChemWebsite.Data;
using ChemWebsite.Data.Dto;
using ChemWebsite.MediatR.CommandAndQuery;

namespace ChemWebsite.API.Helpers
{
    public class PaymentTermProfile : Profile
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public PaymentTermProfile()
        {
            CreateMap<PaymentTerm, PaymentTermDto>().ReverseMap();
            CreateMap<AddPaymentTermCommand, PaymentTerm>();
            CreateMap<UpdatePaymentTermCommand, PaymentTerm>();
        }
    }
}