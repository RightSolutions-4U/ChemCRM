using ChemWebsite.Data.Dto;
using ChemWebsite.Helper;
using MediatR;
using System.Collections.Generic;

namespace ChemWebsite.MediatR.CommandAndQuery
{
    public class GetMonthlyReminderQuery : IRequest<List<CalenderReminderDto>>
    {
        public int Month { get; set; }
        public int Year { get; set; }
    }
}
