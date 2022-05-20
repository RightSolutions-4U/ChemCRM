using ChemWebsite.Data.Dto;
using MediatR;
using System;
using System.Collections.Generic;

namespace ChemWebsite.MediatR.CommandAndQuery
{
    public class GetInquiryActivitiesQuery : IRequest<List<InquiryActivityDto>>
    {
        public Guid InquiryId { get; set; }
    }
}
