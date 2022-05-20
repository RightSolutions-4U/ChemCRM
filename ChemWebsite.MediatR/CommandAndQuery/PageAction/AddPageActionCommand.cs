using ChemWebsite.Data.Dto;
using MediatR;
using System;
using ChemWebsite.Helper;

namespace ChemWebsite.MediatR.CommandAndQuery
{
    public class AddPageActionCommand: IRequest<ServiceResponse<PageActionDto>>
    {
        public Guid PageId { get; set; }
        public Guid ActionId { get; set; }
        public bool Flag { get; set; }
    }
}
