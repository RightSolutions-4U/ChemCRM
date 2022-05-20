using ChemWebsite.Data.Dto;
using ChemWebsite.Data.Entities;
using MediatR;
using System.Collections.Generic;

namespace ChemWebsite.MediatR.CommandAndQuery
{
    public class GetAllInquiryStatusQuery : IRequest<List<InquiryStatusDto>>
    {
    }
}
