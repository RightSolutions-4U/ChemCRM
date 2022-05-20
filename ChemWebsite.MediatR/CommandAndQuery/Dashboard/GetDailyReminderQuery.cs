using ChemWebsite.Data.Dto;
using MediatR;
using System.Collections.Generic;

namespace ChemWebsite.MediatR.CommandAndQuery
{
    public class GetDailyReminderQuery : IRequest<List<CalenderReminderDto>>
    {
        public int Month { get; set; }
        public int Year { get; set; }
    }
}
