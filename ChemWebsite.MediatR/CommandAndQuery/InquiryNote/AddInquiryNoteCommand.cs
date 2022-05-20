using ChemWebsite.Data.Dto;
using ChemWebsite.Helper;
using MediatR;
using System;

namespace ChemWebsite.MediatR.CommandAndQuery
{
    public class AddInquiryNoteCommand: IRequest<ServiceResponse<InquiryNoteDto>>
    {
        public Guid InquiryId { get; set; }
        public string Note { get; set; }
    }
}
