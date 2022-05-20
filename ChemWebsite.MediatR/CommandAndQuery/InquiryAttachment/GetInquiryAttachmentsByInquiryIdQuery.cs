using ChemWebsite.Data.Dto;
using MediatR;
using System;
using System.Collections.Generic;

namespace ChemWebsite.MediatR.CommandAndQuery
{
    public class GetInquiryAttachmentsByInquiryIdQuery : IRequest<List<InquiryAttachmentDto>>
    {
        public Guid InquiryId { get; set; }
    }
}
