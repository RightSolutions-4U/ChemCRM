using ChemWebsite.Data.Dto;
using ChemWebsite.MediatR.CommandAndQuery;
using ChemWebsite.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ChemWebsite.MediatR.Handlers
{
    public class GetMonthlyInquiryQueryHandler
        : IRequestHandler<GetMonthlyInquiryQuery, List<MonthlyInquiryStatisticDto>>
    {

        private readonly IInquiryRepository _inquiryRepository;

        public GetMonthlyInquiryQueryHandler(IInquiryRepository inquiryRepository)
        {
            _inquiryRepository = inquiryRepository;
        }

        public async Task<List<MonthlyInquiryStatisticDto>> Handle(GetMonthlyInquiryQuery request, CancellationToken cancellationToken)
        {
            var data = await _inquiryRepository.All
                .Where(c => c.CreatedDate.Month == request.Month && c.CreatedDate.Year == request.Year)
                .OrderBy(c => c.CreatedDate)
                .GroupBy(c => c.CreatedDate.Date)
                .Select(cs => new MonthlyInquiryStatisticDto
                {
                    Date = cs.Key.ToString("dd/MM/yyyy"),
                    NoOfInquiry = cs.Count()
                }).ToListAsync();
            return data;
        }
    }
}
