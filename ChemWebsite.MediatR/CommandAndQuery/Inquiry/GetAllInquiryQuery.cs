using ChemWebsite.Data;
using ChemWebsite.Data.Resources;
using ChemWebsite.Helper;
using ChemWebsite.Repository;
using MediatR;

namespace ChemWebsite.MediatR.CommandAndQuery
{
    public class GetAllInquiryQuery : IRequest<InquiryList>
    {
        public InquiryResource InquiryResource { get; set; }
    }
}
