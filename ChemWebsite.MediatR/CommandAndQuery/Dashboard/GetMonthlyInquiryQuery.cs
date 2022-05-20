using ChemWebsite.Data.Dto;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChemWebsite.MediatR.CommandAndQuery
{
    public class GetMonthlyInquiryQuery : IRequest<List<MonthlyInquiryStatisticDto>>
    {
        public int Month { get; set; }
        public int Year { get; set; }
    }
}
