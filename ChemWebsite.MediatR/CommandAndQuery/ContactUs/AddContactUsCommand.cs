using ChemWebsite.Data.Dto;
using ChemWebsite.Helper;
using MediatR;

namespace ChemWebsite.MediatR.CommandAndQuery
{
    public class AddContactUsCommand : IRequest<ServiceResponse<ContactUsDto>>
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Message { get; set; }
    }
}
