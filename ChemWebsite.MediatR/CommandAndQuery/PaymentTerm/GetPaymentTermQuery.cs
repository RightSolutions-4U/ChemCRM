using ChemWebsite.Data.Dto;
using ChemWebsite.Helper;
using MediatR;
using System;

namespace ChemWebsite.MediatR.CommandAndQuery
{
   public class GetPaymentTermQuery : IRequest<ServiceResponse<PaymentTermDto>>
    {
        public Guid Id { get; set; }
    }
}

