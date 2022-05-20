using AutoMapper;
using ChemWebsite.Data.Dto;
using ChemWebsite.Data;
using ChemWebsite.MediatR.CommandAndQuery;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChemWebsite.API.Helpers.Mapping
{
    public class PurchaseOrderProfile : Profile
    {
        public PurchaseOrderProfile()
        {
            CreateMap<PurchaseOrderDeliverySchedule, PurchaseOrderDeliveryScheduleDto>().ReverseMap();
            CreateMap<PurchaseOrder, PurchaseOrderDto>().ReverseMap();
            CreateMap<AddPurchaseOrderCommand, PurchaseOrder>();
            CreateMap<UpdateDeliveryScheduleCommand, PurchaseOrderDeliverySchedule>();
            CreateMap<CreateDeliveryScheduleCommand, PurchaseOrderDeliverySchedule>();
            CreateMap<PurchaseOrderAttachment, PurchaseOrderAttachmentDto>().ReverseMap();
        }
    }
}
