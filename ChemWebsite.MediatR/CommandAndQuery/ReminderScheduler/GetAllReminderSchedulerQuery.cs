using ChemWebsite.Data.Dto;
using ChemWebsite.Data.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChemWebsite.MediatR.CommandAndQuery
{
    public class GetAllReminderSchedulerQuery : IRequest<List<ReminderSchedulerDto>>
    {
        public ApplicationEnums Application { get; set; }
        public Guid ReferenceId { get; set; }
    }
}
