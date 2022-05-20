using ChemWebsite.Helper;
using MediatR;
using System;

namespace ChemWebsite.MediatR.CommandAndQuery
{
    public class DeleteInquiryActivityCommand : IRequest<ServiceResponse<bool>>
    {
        public Guid Id { get; set; }
    }
}
