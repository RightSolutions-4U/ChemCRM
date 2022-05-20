using ChemWebsite.Data;
using ChemWebsite.Data.Entities;
using MediatR;
using System.Collections.Generic;

namespace ChemWebsite.MediatR.CommandAndQuery
{
    public class GetPurchaseOrderRecentDeliveryScheduleQuery : IRequest<List<PurchaseOrderRecentDeliverySchedule>>
    {
    }
}
