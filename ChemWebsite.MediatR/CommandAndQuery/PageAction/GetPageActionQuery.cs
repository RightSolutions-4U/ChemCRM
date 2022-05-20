using ChemWebsite.Data.Dto;
using MediatR;
using System;
using ChemWebsite.Helper;

namespace ChemWebsite.MediatR.CommandAndQuery
{
    public class GetPageActionQuery : IRequest<ServiceResponse<PageActionDto>>
    {
        public Guid Id { get; set; }
    }
}
