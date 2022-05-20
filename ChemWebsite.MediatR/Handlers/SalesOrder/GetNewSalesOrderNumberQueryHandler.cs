using ChemWebsite.MediatR.CommandAndQuery;
using ChemWebsite.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace ChemWebsite.MediatR.Handlers
{
    public class GetNewSalesOrderNumberQueryHandler
      : IRequestHandler<GetNewSalesOrderNumberQuery, string>
    {
        private readonly ISalesOrderRepository _salesOrderRepository;

        public GetNewSalesOrderNumberQueryHandler(ISalesOrderRepository salesOrderRepository)
        {
            _salesOrderRepository = salesOrderRepository;
        }
        public async Task<string> Handle(GetNewSalesOrderNumberQuery request, CancellationToken cancellationToken)
        {
            var lastPurchaseOrder = await _salesOrderRepository.All.OrderByDescending(c => c.CreatedDate).FirstOrDefaultAsync();
            if (lastPurchaseOrder == null)
            {
                return "SO#00001";
            }

            var lastPONumber = lastPurchaseOrder.SalesOrderNumber;
            var poId = Regex.Match(lastPONumber, @"\d+").Value;
            var isNumber = int.TryParse(poId, out int poNumber);
            if (isNumber)
            {
                var newPoId = lastPONumber.Replace(poNumber.ToString(), "");
                return $"{newPoId}{poNumber + 1}";
            }
            else
            {
                return $"{lastPONumber}#00001";
            }
        }
    }
}
