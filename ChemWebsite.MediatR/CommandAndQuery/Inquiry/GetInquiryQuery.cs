using ChemWebsite.Data.Dto;
using ChemWebsite.Helper;
using MediatR;
using System;

namespace ChemWebsite.MediatR.CommandAndQuery
{
    public class GetInquiryQuery : IRequest<ServiceResponse<InquiryDto>>
    {
        public Guid Id { get; set; }
    }
}
