using ChemWebsite.Data.Dto;
using MediatR;
using System;
using ChemWebsite.Helper;

namespace ChemWebsite.MediatR.CommandAndQuery
{
    public class DeletePageActionCommand : IRequest<ServiceResponse<PageActionDto>>
    {
        public Guid Id { get; set; }
        public Guid PageId { get; set; }
        public Guid ActionId { get; set; }
    }
}
