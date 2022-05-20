using ChemWebsite.Data.Dto;
using ChemWebsite.MediatR.Handlers;
using MediatR;


namespace ChemWebsite.MediatR.CommandAndQuery
{
    public class GetAllInventoryQuery : IRequest<InventoryList>
    {
        public string ChemicalName { get; set; }
        public string CasNo { get; set; }
        public int PageSize { get; set; } = 20;
        public int Skip { get; set; } = 0;
    }
}
