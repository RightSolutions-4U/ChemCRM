using ChemWebsite.Data;
using ChemWebsite.Data.Dto;
using ChemWebsite.Data.Resources;
using ChemWebsite.Helper;
using MediatR;
using System.Collections.Generic;

namespace ChemWebsite.MediatR.CommandAndQuery
{
    public class GetAllContactUsQuery : IRequest<PagedList<ContactRequest>>
    {
        public ContactUsResource ContactUsResource { get; set; }
    }
}
