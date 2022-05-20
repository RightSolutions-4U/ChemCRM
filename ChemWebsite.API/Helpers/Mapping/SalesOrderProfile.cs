using AutoMapper;
using ChemWebsite.Data;
using ChemWebsite.Data.Dto;
using ChemWebsite.MediatR.CommandAndQuery;

namespace ChemWebsite.API.Helpers.Mapping
{
    public class SalesOrderProfile : Profile
    {
        public SalesOrderProfile()
        {
            CreateMap<SalesPurchaseOrderItem, SalesPurchaseOrderItemDto>().ReverseMap();
            CreateMap<SalesOrder, SalesOrderDto>().ReverseMap();
            CreateMap<AddSalesOrderCommand, SalesOrder>();
            CreateMap<SalesOrderAttachment, SalesOrderAttachmentDto>().ReverseMap();
        }
    }
}
