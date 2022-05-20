using MediatR;
using System;

namespace ChemWebsite.MediatR.CommandAndQuery
{
    public class IsEmailOrPhoneExistQuery: IRequest<Boolean>
    {
        public Guid Id { get; set; }
        public string EMail { get; set; }
        public string Phone { get; set; }
    }
}
