using ChemWebsite.Data.Dto;
using ChemWebsite.Helper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChemWebsite.MediatR.CommandAndQuery
{
    public class GetInquiryNotesQuery : IRequest<List<InquiryNoteDto>>
    {
        public Guid InquiryId { get; set; }
    }
}
